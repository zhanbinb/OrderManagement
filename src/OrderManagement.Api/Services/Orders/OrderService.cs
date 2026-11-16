using OrderManagement.Api.Data;
using OrderManagement.Api.DTOs.Orders;
using OrderManagement.Api.Entities;
using OrderManagement.Api.Enums;

namespace OrderManagement.Api.Services.Orders;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _repository;
    private readonly ILogger<OrderService> _logger;

    public OrderService(IOrderRepository repository, ILogger<OrderService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Order> CreateAsync(CreateOrderRequest request)
    {
        _logger.LogInformation("开始创建订单，订单编号：{OrderNo}", request.OrderNo);
        var order = new Order
        {
            CustomerName = request.CustomerName,
            OrderNo = request.OrderNo,
            TotalAmount = request.TotalAmount,
            Status = OrderStatus.Pending,
        };
        await _repository.AddAsync(order);
        _logger.LogInformation("订单创建成功，订单编号：{OrderNo}", order.OrderNo);
        return order;
    }
}