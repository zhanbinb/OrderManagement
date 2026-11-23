namespace OrderManagement.Api.Messaging.Messages;

public class OrderCreatedMessage
{
    public Guid MessageId { get; set; } = Guid.NewGuid();
    public int OrderId { get; set; }
    public decimal Amount { get; set; }
    public DateTime CreatedAt { get; set; }
}