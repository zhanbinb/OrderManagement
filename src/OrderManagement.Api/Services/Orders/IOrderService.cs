using OrderManagement.Api.DTOs.Orders;
using OrderManagement.Api.Entities;

namespace OrderManagement.Api.Services.Orders;

public interface IOrderService
{
    Task<Order> CreateAsync(CreateOrderRequest request);

    Task<OrderDetailDto?> GetByIdAsync(int id);

    Task<OrderListResponse> GetListAsync(int pageIndex, int pageSize);

    Task<bool> UpdateAsync(int id, UpdateOrderRequest request);

    Task<bool> DeleteAsync(int id);

    Task<List<OrderListItemDto>> GetPendingAsync();
}
