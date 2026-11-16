using OrderManagement.Api.Data;
using OrderManagement.Api.DTOs.Orders;
using OrderManagement.Api.Entities;
using OrderManagement.Api.Enums;

namespace OrderManagement.Api.Services.Orders;

public class OrderService : IOrderService
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<OrderService> _logger;

    public OrderService(ApplicationDbContext context, ILogger<OrderService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Order> CreateAsync(CreateOrderRequest request)
    {
        _logger.LogInformation("开始创建订单，订单编号：{OrderNo}", request.OrderNo);
        var order = new Order
        {
            OrderNo = request.OrderNo,
            CustomerName = request.CustomerName,
            TotalAmount = request.TotalAmount,
            Status = OrderStatus.Pending,
        };
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
        _logger.LogInformation("订单创建成功，订单编号：{OrderNo}", order.OrderNo);
        return order;
    }
}