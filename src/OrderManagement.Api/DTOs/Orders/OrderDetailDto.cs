using OrderManagement.Api.Enums;

namespace OrderManagement.Api.DTOs.Orders;

public class OrderDetailDto
{
    public int Id { get; set; }
    public string OrderNo { get; set; } = string.Empty;
    public string CustomerName { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
    public OrderStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    
    public uint Version { get; set; }
}
