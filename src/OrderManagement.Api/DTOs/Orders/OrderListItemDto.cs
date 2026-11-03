using OrderManagement.Api.Enums;

namespace OrderManagement.Api.DTOs.Orders;

public class OrderListItemDto
{
    public int Id { get; set; }
    public string OrderNo { get; set; } = string.Empty;
    public string CustomerName { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
    public OrderStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
}