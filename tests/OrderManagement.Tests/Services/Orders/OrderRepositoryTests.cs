using Microsoft.EntityFrameworkCore;
using OrderManagement.Api.Data;
using OrderManagement.Api.Entities;
using OrderManagement.Api.Enums;
using OrderManagement.Api.Services.Orders;
using Xunit;

namespace OrderManagement.Tests;

public class OrderRepositoryTests
{
    [Fact]
    public async Task AddAsync_ValidOrder_ShouldSaveToDatabase()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        await using var context = new ApplicationDbContext(options);

        var repository = new OrderRepository(context);

        var order = new Order
        {
            OrderNo = "ORD-REPO-001",
            CustomerName = "张三",
            TotalAmount = 200m,
            Status = OrderStatus.Pending
        };

        // Act
        await repository.AddAsync(order);

        // Assert
        var savedOrder = await context.Orders
            .FirstOrDefaultAsync(x => x.OrderNo == "ORD-REPO-001");

        Assert.NotNull(savedOrder);
        Assert.Equal("张三", savedOrder.CustomerName);
        Assert.Equal(200m, savedOrder.TotalAmount);
        Assert.Equal(OrderStatus.Pending, savedOrder.Status);
    }
}