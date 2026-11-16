using OrderManagement.Api.Entities;

namespace OrderManagement.Api.Services.Orders;

public interface IOrderRepository
{
    Task AddAsync(Order order);
}