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
        await using var context = CreateContext();
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

    [Fact]
    public async Task GetByIdAsync_WhenOrderExists_ShouldReturnOrder()
    {
        // Arrange
        await using var context = CreateContext();
        var order = new Order { OrderNo = "ORD-REPO-002", CustomerName = "李四", TotalAmount = 50m };
        context.Orders.Add(order);
        await context.SaveChangesAsync();

        var repository = new OrderRepository(context);

        // Act
        var result = await repository.GetByIdAsync(order.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("ORD-REPO-002", result.OrderNo);
    }

    [Fact]
    public async Task GetByIdAsync_WhenOrderNotExists_ShouldReturnNull()
    {
        // Arrange
        await using var context = CreateContext();
        var repository = new OrderRepository(context);

        // Act
        var result = await repository.GetByIdAsync(999);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task CountAsync_ShouldReturnTotalCount()
    {
        // Arrange
        await using var context = CreateContext();
        context.Orders.AddRange(
            new Order { OrderNo = "ORD-REPO-003" },
            new Order { OrderNo = "ORD-REPO-004" },
            new Order { OrderNo = "ORD-REPO-005" });
        await context.SaveChangesAsync();

        var repository = new OrderRepository(context);

        // Act
        var count = await repository.CountAsync();

        // Assert
        Assert.Equal(3, count);
    }

    [Fact]
    public async Task GetPagedAsync_ShouldReturnRequestedPageOrderedByCreatedAtDescending()
    {
        // Arrange
        await using var context = CreateContext();

        for (var i = 0; i < 5; i++)
        {
            context.Orders.Add(new Order
            {
                OrderNo = $"ORD-PAGE-{i}",
                CreatedAt = new DateTime(2026, 1, 1).AddDays(i)
            });
        }

        await context.SaveChangesAsync();

        var repository = new OrderRepository(context);

        // Act
        var page = await repository.GetPagedAsync(pageIndex: 2, pageSize: 2);

        // Assert
        Assert.Equal(2, page.Count);
        Assert.Equal("ORD-PAGE-2", page[0].OrderNo);
        Assert.Equal("ORD-PAGE-1", page[1].OrderNo);
    }

    [Fact]
    public async Task GetByStatusAsync_ShouldReturnOnlyMatchingOrders()
    {
        // Arrange
        await using var context = CreateContext();
        context.Orders.AddRange(
            new Order { OrderNo = "ORD-STATUS-001", Status = OrderStatus.Pending },
            new Order { OrderNo = "ORD-STATUS-002", Status = OrderStatus.Paid },
            new Order { OrderNo = "ORD-STATUS-003", Status = OrderStatus.Pending });
        await context.SaveChangesAsync();

        var repository = new OrderRepository(context);

        // Act
        var result = await repository.GetByStatusAsync(OrderStatus.Pending);

        // Assert
        Assert.Equal(2, result.Count);
        Assert.All(result, x => Assert.Equal(OrderStatus.Pending, x.Status));
    }

    [Fact]
    public async Task UpdateAsync_ShouldPersistChanges()
    {
        // Arrange
        await using var context = CreateContext();
        var order = new Order { OrderNo = "ORD-REPO-006", CustomerName = "旧客户", TotalAmount = 10m };
        context.Orders.Add(order);
        await context.SaveChangesAsync();

        var repository = new OrderRepository(context);

        // Act
        order.CustomerName = "新客户";
        order.TotalAmount = 99m;
        await repository.UpdateAsync(order);

        // Assert
        var updated = await context.Orders
            .AsNoTracking()
            .FirstAsync(x => x.Id == order.Id);

        Assert.Equal("新客户", updated.CustomerName);
        Assert.Equal(99m, updated.TotalAmount);
    }

    [Fact]
    public async Task DeleteAsync_ShouldRemoveOrder()
    {
        // Arrange
        await using var context = CreateContext();
        var order = new Order { OrderNo = "ORD-REPO-007" };
        context.Orders.Add(order);
        await context.SaveChangesAsync();

        var repository = new OrderRepository(context);

        // Act
        await repository.DeleteAsync(order);

        // Assert
        var exists = await context.Orders
            .AsNoTracking()
            .AnyAsync(x => x.Id == order.Id);

        Assert.False(exists);
    }

    private static ApplicationDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new ApplicationDbContext(options);
    }
}
