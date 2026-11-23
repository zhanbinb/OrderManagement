using OrderManagement.Domain.Aggregates;
using OrderManagement.Domain.Repositories;
using OrderManagement.Domain.ValueObjects;

namespace OrderManagement.Application.Services;

public class OrderApplicationService
{
    private readonly IOrderRepository _orderRepository;

    public OrderApplicationService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }
    
    public async Task<long> CreateOrderAsync(
        CreateOrderRequest request,
        CancellationToken cancellationToken = default)
    {
        var order = new Order();

        foreach (var item in request.Items)
        {
            order.AddItem(
                item.ProductId,
                item.ProductName,
                new Money(item.UnitPrice, item.Currency),
                item.Quantity);
        }

        await _orderRepository.AddAsync(order, cancellationToken);

        await _orderRepository.SaveChangesAsync(cancellationToken);

        return order.Id;
    }
    
    public async Task<OrderDetailResponse> GetOrderDetailAsync(
        long orderId,
        CancellationToken cancellationToken = default)
    {
        var order = await _orderRepository.GetByIdAsync(orderId, cancellationToken);

        if (order is null)
        {
            return null;
        }
        var total = order.TotalAmount;
        return new OrderDetailResponse
        {
            OrderId = orderId,
            Status = (int)order.Status,
            TotalAmount = total.Amount,
            Currency = total.Currency,
            Items = order.Items.Select(item => new OrderItemResponse
            {
                Id = item.Id,
                ProductId = item.ProductId,
                ProductName = item.ProductName,
                UnitPrice = item.UnitPrice.Amount,
                Quantity = item.Quantity,
                Subtotal = item.Subtotal.Amount
            }).ToList()
        };
    }
    
    public async Task<bool> UpdateOrderItemQuantityAsync(
        long orderId,
        Guid itemId,
       int quantity,
        CancellationToken cancellationToken = default)
    {
        var order = await _orderRepository.GetByIdAsync(orderId, cancellationToken);

        if (order == null)
            return false;

        order.UpdateItemQuantity(itemId, quantity);
        await _orderRepository.SaveChangesAsync(cancellationToken);

        return true;
    }
    
    public async Task<bool> CancelOrderAsync(
        long orderId,
        CancellationToken cancellationToken = default)
    {
        var order = await _orderRepository.GetByIdAsync(
            orderId, cancellationToken);

        if (order == null)
            return false;

        order.Cancel();

        await _orderRepository.SaveChangesAsync(cancellationToken);

        return true;
    }
}