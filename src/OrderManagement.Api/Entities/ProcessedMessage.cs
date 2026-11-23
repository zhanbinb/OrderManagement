namespace OrderManagement.Api.Entities;

public class ProcessedMessage
{
    public long Id { get; set; }

    public Guid MessageId { get; set; }

    public DateTime ProcessedAt { get; set; } = DateTime.UtcNow;
    
}