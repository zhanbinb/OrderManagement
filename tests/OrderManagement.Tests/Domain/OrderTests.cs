using OrderManagement.Domain.Aggregates;
using OrderManagement.Domain.Enums;
using OrderManagement.Domain.Exceptions;
using OrderManagement.Domain.ValueObjects;

namespace OrderManagement.Tests.Domain;

public class OrderTests
{
    [Fact]
    public void NewOrder_ShouldHavePendingStatus()
    {
        // Arrange
        var order = new Order();

        // Assert
        Assert.Equal(OrderStatus.Pending, order.Status);
    }

    [Fact]
    public void Pay_EmptyOrder_ShouldThrowException()
    {
        // Arrange
        var order = new Order();

        // Act & Assert
        Assert.Throws<BusinessException>(() => order.Pay());
    }

    [Fact]
    public void Pay_ValidOrder_ShouldChangeStatusToPaid()
    {
        // Arrange
        var order = new Order();
        order.AddItem(
            1001,
            "Keyboard",
            new Money(200, "CNY"),
            1);

        // Act
        order.Pay();

        // Assert
        Assert.Equal(OrderStatus.Paid, order.Status);
    }

    [Fact]
    public void Cancel_PendingOrder_ShouldChangeStatusToCancelled()
    {
        // Arrange
        var order = new Order();

        // Act
        order.Cancel();

        // Assert
        Assert.Equal(OrderStatus.Cancelled, order.Status);
    }

    [Fact]
    public void UpdateQuantity_NonPositiveQuantity_ShouldThrowException()
    {
        // Arrange
        var order = new Order();
        order.AddItem(
            1001,
            "Keyboard",
            new Money(200, "CNY"),
            1);

        var itemId = order.Items.First().Id;

        // Act & Assert
        Assert.Throws<ArgumentException>(
            () => order.UpdateItemQuantity(itemId, 0));
    }

    [Fact]
    public void UpdateQuantity_PaidOrder_ShouldThrowException()
    {
        // Arrange
        var order = new Order();
        order.AddItem(
            1001,
            "Keyboard",
            new Money(200, "CNY"),
            1);

        var itemId = order.Items.First().Id;
        order.Pay();

        // Act & Assert
        Assert.Throws<BusinessException>(
            () => order.UpdateItemQuantity(itemId, 2));
    }
}