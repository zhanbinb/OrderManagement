using OrderManagement.Domain.Aggregates;
using OrderManagement.Domain.ValueObjects;

namespace OrderManagement.Tests;

public class UnitTest1
{
    [Fact]
    public void PaidOrder_ShouldNotAllowAddingItem()
    {
        var order = new Order();

        order.AddItem(
            1001,
            "iPhone",
            new Money(5000, "CNY"),
            1);

        order.Pay();

        Assert.Throws<InvalidOperationException>(() =>
            order.AddItem(
                1002,
                "MacBook",
                new Money(10000, "CNY"),
                1));
    }
}
