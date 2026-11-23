using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Enums;
using OrderManagement.Domain.Exceptions;
using OrderManagement.Domain.ValueObjects;

namespace OrderManagement.Domain.Aggregates;

public class Order
{
    private readonly List<OrderItem> _items = [];

    public long Id { get; private set; }
    public OrderStatus Status { get; private set; }
    public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();

    public Money TotalAmount
    {
        get
        {
            var total = new Money(0, "CNY");
            foreach (var item in _items)
            {
                total = total.Add(item.Subtotal);
            }

            return total;
        }
    }

    // private Order(bool forEfCore)
    // {
    // }

    public Order()
    {
        Status = OrderStatus.Pending;
    }

    public void AddItem(
        long productId,
        string productName,
        Money unitPrice,
        int quantity)
    {
        EnsurePending();

        var item = new OrderItem(
            productId,
            productName,
            unitPrice,
            quantity);

        _items.Add(item);
    }

    public void UpdateItemQuantity(Guid itemId, int quantity)
    {
        EnsurePending();

        var item = _items.FirstOrDefault(x => x.Id == itemId)
                   ?? throw new InvalidOperationException("Order item not found");

        item.UpdateQuantity(quantity);
    }

    public void Pay()
    {
        EnsurePending();

        if (_items.Count == 0)
            throw new BusinessException("Cannot pay an empty order");

        Status = OrderStatus.Paid;
    }

    public void Cancel()
    {
        EnsurePending();

        Status = OrderStatus.Cancelled;
    }

    private void EnsurePending()
    {
        if (Status != OrderStatus.Pending)
            throw new BusinessException(
                "Only pending orders can be modified");
    }
}