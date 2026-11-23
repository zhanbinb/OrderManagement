using Microsoft.AspNetCore.Mvc;
using OrderManagement.Api.Messaging;
using OrderManagement.Api.Messaging.Messages;

namespace OrderManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MessageController : ControllerBase
{
    private readonly RabbitMqProducer _producer;

    public MessageController(RabbitMqProducer producer)
    {
        _producer = producer;
    }

    [HttpPost("publish")]
    public async Task<IActionResult> Publish(
        CancellationToken cancellationToken)
    {
        var message = new OrderCreatedMessage
        {
            OrderId = 1001,
            Amount = 199.00m,
            CreatedAt = DateTime.UtcNow
        };

        await _producer.PublishAsync(message, cancellationToken);

        return Ok(new
        {
            Message = "Order message published successfully"
        });
    }
}