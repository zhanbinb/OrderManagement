namespace OrderManagement.Application;

public class OrderDetailResponse
{
    public long OrderId { get; set; }

    public int Status { get; set; }

    public decimal TotalAmount { get; set; }

    public string Currency { get; set; } = "CNY";

    public List<OrderItemResponse> Items { get; set; } = [];
}

public class OrderItemResponse
{
    public Guid Id { get; set; }

    public long ProductId { get; set; }

    public string ProductName { get; set; } = string.Empty;

    public decimal UnitPrice { get; set; }

    public int Quantity { get; set; }

    public decimal Subtotal { get; set; }
}