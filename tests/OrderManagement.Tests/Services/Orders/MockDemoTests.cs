using Moq;
using Xunit;

namespace OrderManagement.Tests.Services.Orders;

public class MockDemoTests
{
    [Fact]
    public void MockExample_ShouldReturnConfiguredValue()
    {
        // Arrange
        var mock = new Mock<IOrderNumberGenerator>();

        mock.Setup(x => x.Generate())
            .Returns("ORD-20261116-001");

        // Act
        var orderNo = mock.Object.Generate();

        // Assert
        Assert.Equal("ORD-20261116-001", orderNo);

        mock.Verify(x => x.Generate(), Times.Once);
    }
}

public interface IOrderNumberGenerator
{
    string Generate();
}