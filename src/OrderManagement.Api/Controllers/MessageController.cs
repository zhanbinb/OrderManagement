using Microsoft.AspNetCore.Mvc;
using OrderManagement.Api.Messaging;
using OrderManagement.Api.Messaging.Messages;

namespace OrderManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MessageController : ControllerBase
{
    private readonly RabbitMqProducer _producer;
    private static readonly Guid TestMessageId =
        Guid.NewGuid();
    private readonly ILogger<MessageController> _logger;

    public MessageController(RabbitMqProducer producer, ILogger<MessageController> logger)
    {
        _producer = producer;
        _logger = logger;
    }

    [HttpPost("publish")]
    public async Task<IActionResult> Publish(
        CancellationToken cancellationToken)
    {
        var message = new OrderCreatedMessage
        {
            MessageId = TestMessageId,
            OrderId = 1001,
            Amount = 199.00m,
            CreatedAt = DateTime.UtcNow
        };
        _logger.LogInformation("Publishing message with ID: {MessageId}", message.MessageId);
        await _producer.PublishAsync(message, cancellationToken);

        return Ok(new
        {
            Message = "Order message published successfully"
        });
    }
}