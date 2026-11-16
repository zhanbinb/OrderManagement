using Microsoft.Extensions.Logging.Abstractions;
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
}