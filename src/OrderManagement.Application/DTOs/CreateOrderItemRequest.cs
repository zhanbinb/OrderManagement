namespace OrderManagement.Application;

public class CreateOrderItemRequest
{
    public long ProductId { get; set; }

    public string ProductName { get; set; } = string.Empty;

    public decimal UnitPrice { get; set; }

    public string Currency { get; set; } = "CNY";

    public int Quantity { get; set; }
}