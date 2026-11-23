using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using OrderManagement.Api.Data;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using OrderManagement.Api.Messaging.Messages;

namespace OrderManagement.Api.Messaging.Consumers;

public class OrderCreatedConsumer : BackgroundService
{
    private readonly RabbitMqOptions _options;
    private readonly ILogger<OrderCreatedConsumer> _logger;
    private readonly IServiceScopeFactory _scopeFactory;

    public OrderCreatedConsumer(
        IOptions<RabbitMqOptions> options,
        ILogger<OrderCreatedConsumer> logger,
        IServiceScopeFactory scopeFactory)
    {
        _options = options.Value;
        _logger = logger;
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(
        CancellationToken stoppingToken)
    {
        var factory = new ConnectionFactory
        {
            HostName = _options.Host,
            Port = _options.Port,
            UserName = _options.UserName,
            Password = _options.Password
        };

        await using var connection =
            await factory.CreateConnectionAsync(stoppingToken);

        await using var channel =
            await connection.CreateChannelAsync(
                cancellationToken: stoppingToken);

        // 确保队列存在
        await channel.QueueDeclareAsync(
            queue: _options.Queue,
            durable: true,
            exclusive: false,
            autoDelete: false,
            cancellationToken: stoppingToken);

        // 创建异步消费者
        var consumer = new AsyncEventingBasicConsumer(channel);

consumer.ReceivedAsync += async (_, ea) =>
{
    try
    {
        var message =
            JsonSerializer.Deserialize<OrderCreatedMessage>(
                ea.Body.Span);

        if (message is null || message.MessageId == Guid.Empty)
        {
            throw new InvalidOperationException(
                "Invalid order message.");
        }

        // 每条消息创建独立 Scope
        await using var scope = _scopeFactory.CreateAsyncScope();

        var dbContext = scope.ServiceProvider
            .GetRequiredService<ApplicationDbContext>();

        // 开启数据库事务
        await using var transaction =
            await dbContext.Database.BeginTransactionAsync(stoppingToken);

        // 尝试记录 MessageId
        var inserted = await dbContext.Database.ExecuteSqlInterpolatedAsync(
            $"""
            INSERT INTO "ProcessedMessages" ("MessageId", "ProcessedAt")
            VALUES ({message.MessageId}, {DateTime.UtcNow})
            ON CONFLICT ("MessageId") DO NOTHING
            """,
            stoppingToken);

        // inserted == 0，说明消息已经处理过
        if (inserted == 0)
        {
            _logger.LogInformation(
                "Duplicate message detected. MessageId: {MessageId}",
                message.MessageId);

            await transaction.CommitAsync(stoppingToken);

            await channel.BasicAckAsync(
                deliveryTag: ea.DeliveryTag,
                multiple: false);

            return;
        }

        // 第一次收到消息，执行实际业务逻辑
        _logger.LogInformation(
            "Processing order. OrderId: {OrderId}, Amount: {Amount}",
            message.OrderId,
            message.Amount);

        // TODO：在这里执行实际数据库业务操作
        // 例如：更新订单状态、增加积分等。
        // 所有数据库操作必须使用当前 dbContext 和当前事务。

        // 提交事务
        await transaction.CommitAsync(stoppingToken);

        // 数据库事务成功后再 ACK
        await channel.BasicAckAsync(
            deliveryTag: ea.DeliveryTag,
            multiple: false);
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "Failed to process order message.");

        await channel.BasicNackAsync(
            deliveryTag: ea.DeliveryTag,
            multiple: false,
            requeue: false);
    }
};

        // 开始监听队列，关闭自动 ACK
        await channel.BasicConsumeAsync(
            queue: _options.Queue,
            autoAck: false,
            consumer: consumer);

        _logger.LogInformation(
            "OrderCreatedConsumer started. Queue: {Queue}",
            _options.Queue);

        // 持续运行，直到应用程序停止
        await Task.Delay(Timeout.Infinite, stoppingToken);
    }
}