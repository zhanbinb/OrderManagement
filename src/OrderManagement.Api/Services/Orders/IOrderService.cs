using OrderManagement.Api.DTOs.Orders;
using OrderManagement.Api.Entities;

namespace OrderManagement.Api.Services.Orders;

public interface IOrderService
{
    Task<Order> CreateAsync(CreateOrderRequest request);
}