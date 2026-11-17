using OrderManagement.Api.Entities;
using OrderManagement.Api.Enums;

namespace OrderManagement.Api.Services.Orders;

public interface IOrderRepository
{
    Task AddAsync(Order order);

    Task<Order?> GetByIdAsync(int id, bool asNoTracking = true);

    Task<int> CountAsync();

    Task<List<Order>> GetPagedAsync(int pageIndex, int pageSize);

    Task<List<Order>> GetByStatusAsync(OrderStatus status);

    Task UpdateAsync(Order order);

    Task DeleteAsync(Order order);
}
