using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Moq;
using OrderManagement.Api.DTOs.Orders;
using OrderManagement.Api.Entities;
using OrderManagement.Api.Enums;
using OrderManagement.Api.Services.Orders;
using Xunit;

namespace OrderManagement.Tests;

public class OrderServiceTests
{
    [Fact]
    public async Task CreateAsync_ValidRequest_ShouldCreateOrderAndCallRepository()
    {
        // Arrange
        var repositoryMock = new Mock<IOrderRepository>();

        Order? savedOrder = null;

        repositoryMock
            .Setup(x => x.AddAsync(It.IsAny<Order>()))
            .Callback<Order>(order => savedOrder = order)
            .Returns(Task.CompletedTask);

        var service = new OrderService(
            repositoryMock.Object,
            CreateCache(),
            NullLogger<OrderService>.Instance);

        var request = new CreateOrderRequest
        {
            OrderNo = "ORD-TEST-001",
            CustomerName = "张三",
            TotalAmount = 100.50m
        };

        // Act
        var result = await service.CreateAsync(request);

        // Assert
        Assert.NotNull(savedOrder);

        Assert.Equal(request.OrderNo, result.OrderNo);
        Assert.Equal(request.CustomerName, result.CustomerName);
        Assert.Equal(request.TotalAmount, result.TotalAmount);
        Assert.Equal(OrderStatus.Pending, result.Status);

        // 验证 Repository 被调用一次
        repositoryMock.Verify(
            x => x.AddAsync(It.IsAny<Order>()),
            Times.Once);
    }

    [Fact]
    public async Task GetByIdAsync_WhenCacheMiss_ShouldQueryRepositoryAndCacheResult()
    {
        // Arrange
        var repositoryMock = new Mock<IOrderRepository>();

        repositoryMock
            .Setup(x => x.GetByIdAsync(1, true))
            .ReturnsAsync(new Order
            {
                Id = 1,
                OrderNo = "ORD-001",
                CustomerName = "张三",
                TotalAmount = 100m,
                Status = OrderStatus.Pending
            });

        var service = new OrderService(
            repositoryMock.Object,
            CreateCache(),
            NullLogger<OrderService>.Instance);

        // Act
        var first = await service.GetByIdAsync(1);
        var second = await service.GetByIdAsync(1);

        // Assert
        Assert.NotNull(first);
        Assert.Equal("ORD-001", first.OrderNo);
        Assert.Equal("张三", first.CustomerName);

        Assert.NotNull(second);
        Assert.Equal("ORD-001", second.OrderNo);

        // 第二次应从缓存返回，Repository 只被查询一次
        repositoryMock.Verify(x => x.GetByIdAsync(1, true), Times.Once);
    }

    [Fact]
    public async Task GetByIdAsync_WhenOrderNotExists_ShouldCacheNullAndReturnNull()
    {
        // Arrange
        var repositoryMock = new Mock<IOrderRepository>();

        repositoryMock
            .Setup(x => x.GetByIdAsync(9, true))
            .ReturnsAsync((Order?)null);

        var service = new OrderService(
            repositoryMock.Object,
            CreateCache(),
            NullLogger<OrderService>.Instance);

        // Act
        var first = await service.GetByIdAsync(9);
        var second = await service.GetByIdAsync(9);

        // Assert
        Assert.Null(first);
        Assert.Null(second);

        // 空值缓存生效，Repository 只被查询一次
        repositoryMock.Verify(x => x.GetByIdAsync(9, true), Times.Once);
    }

    [Fact]
    public async Task GetListAsync_ShouldReturnPagedResponse()
    {
        // Arrange
        var repositoryMock = new Mock<IOrderRepository>();

        repositoryMock.Setup(x => x.CountAsync()).ReturnsAsync(25);
        repositoryMock
            .Setup(x => x.GetPagedAsync(2, 10))
            .ReturnsAsync(new List<Order>
            {
                new() { Id = 11, OrderNo = "ORD-011", CustomerName = "张三", TotalAmount = 10m },
                new() { Id = 12, OrderNo = "ORD-012", CustomerName = "李四", TotalAmount = 20m }
            });

        var service = new OrderService(
            repositoryMock.Object,
            CreateCache(),
            NullLogger<OrderService>.Instance);

        // Act
        var result = await service.GetListAsync(2, 10);

        // Assert
        Assert.Equal(25, result.TotalCount);
        Assert.Equal(2, result.PageIndex);
        Assert.Equal(10, result.PageSize);
        Assert.Equal(3, result.TotalPages);
        Assert.Equal(2, result.Items.Count);
        Assert.Equal("ORD-011", result.Items[0].OrderNo);
        Assert.Equal(OrderStatus.Pending, result.Items[0].Status);
    }

    [Fact]
    public async Task UpdateAsync_WhenOrderExists_ShouldUpdateAndInvalidateCache()
    {
        // Arrange
        var repositoryMock = new Mock<IOrderRepository>();
        var existingOrder = new Order
        {
            Id = 1,
            OrderNo = "ORD-001",
            CustomerName = "旧客户",
            TotalAmount = 10m
        };

        repositoryMock
            .Setup(x => x.GetByIdAsync(1, false))
            .ReturnsAsync(existingOrder);
        repositoryMock
            .Setup(x => x.UpdateAsync(It.IsAny<Order>()))
            .Returns(Task.CompletedTask);

        var cache = CreateCache();
        await cache.SetStringAsync("order:1", "cached-value");

        var service = new OrderService(
            repositoryMock.Object,
            cache,
            NullLogger<OrderService>.Instance);

        var request = new UpdateOrderRequest
        {
            CustomerName = "新客户",
            TotalAmount = 99m,
            Version = existingOrder.Version
        };

        // Act
        var updated = await service.UpdateAsync(1, request);

        // Assert
        Assert.True(updated);
        Assert.Equal("新客户", existingOrder.CustomerName);
        Assert.Equal(99m, existingOrder.TotalAmount);
        Assert.NotNull(existingOrder.UpdatedAt);

        repositoryMock.Verify(x => x.UpdateAsync(existingOrder), Times.Once);
        Assert.Null(await cache.GetStringAsync("order:1"));
    }

    [Fact]
    public async Task UpdateAsync_WhenOrderNotExists_ShouldReturnFalse()
    {
        // Arrange
        var repositoryMock = new Mock<IOrderRepository>();

        repositoryMock
            .Setup(x => x.GetByIdAsync(99, false))
            .ReturnsAsync((Order?)null);

        var service = new OrderService(
            repositoryMock.Object,
            CreateCache(),
            NullLogger<OrderService>.Instance);

        // Act
        var updated = await service.UpdateAsync(99, new UpdateOrderRequest
        {
            CustomerName = "新客户",
            TotalAmount = 99m
        });

        // Assert
        Assert.False(updated);
        repositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Order>()), Times.Never);
    }

    [Fact]
    public async Task DeleteAsync_WhenOrderExists_ShouldDeleteAndInvalidateCache()
    {
        // Arrange
        var repositoryMock = new Mock<IOrderRepository>();
        var existingOrder = new Order
        {
            Id = 1,
            OrderNo = "ORD-001",
            CustomerName = "张三",
            TotalAmount = 100m
        };

        repositoryMock
            .Setup(x => x.GetByIdAsync(1, false))
            .ReturnsAsync(existingOrder);
        repositoryMock
            .Setup(x => x.DeleteAsync(It.IsAny<Order>()))
            .Returns(Task.CompletedTask);

        var cache = CreateCache();
        await cache.SetStringAsync("order:1", "cached-value");

        var service = new OrderService(
            repositoryMock.Object,
            cache,
            NullLogger<OrderService>.Instance);

        // Act
        var deleted = await service.DeleteAsync(1);

        // Assert
        Assert.True(deleted);
        repositoryMock.Verify(x => x.DeleteAsync(existingOrder), Times.Once);
        Assert.Null(await cache.GetStringAsync("order:1"));
    }

    [Fact]
    public async Task DeleteAsync_WhenOrderNotExists_ShouldReturnFalse()
    {
        // Arrange
        var repositoryMock = new Mock<IOrderRepository>();

        repositoryMock
            .Setup(x => x.GetByIdAsync(99, false))
            .ReturnsAsync((Order?)null);

        var service = new OrderService(
            repositoryMock.Object,
            CreateCache(),
            NullLogger<OrderService>.Instance);

        // Act
        var deleted = await service.DeleteAsync(99);

        // Assert
        Assert.False(deleted);
        repositoryMock.Verify(x => x.DeleteAsync(It.IsAny<Order>()), Times.Never);
    }

    [Fact]
    public async Task GetPendingAsync_ShouldReturnMappedList()
    {
        // Arrange
        var repositoryMock = new Mock<IOrderRepository>();

        repositoryMock
            .Setup(x => x.GetByStatusAsync(OrderStatus.Pending))
            .ReturnsAsync(new List<Order>
            {
                new() { Id = 1, OrderNo = "ORD-001", CustomerName = "张三", TotalAmount = 10m },
                new() { Id = 2, OrderNo = "ORD-002", CustomerName = "李四", TotalAmount = 20m }
            });

        var service = new OrderService(
            repositoryMock.Object,
            CreateCache(),
            NullLogger<OrderService>.Instance);

        // Act
        var result = await service.GetPendingAsync();

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Equal("ORD-001", result[0].OrderNo);
        Assert.Equal("ORD-002", result[1].OrderNo);
    }

    private static IDistributedCache CreateCache()
    {
        return new MemoryDistributedCache(
            Options.Create(new MemoryDistributedCacheOptions()));
    }
}
