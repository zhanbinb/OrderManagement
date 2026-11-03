using OrderManagement.Api.DTOs.Orders;

namespace OrderManagement.Api.DTOs.Orders;

public class OrderListResponse
{
    public int TotalCount { get; set; }
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public List<OrderListItemDto> Items { get; set; } = new();
}