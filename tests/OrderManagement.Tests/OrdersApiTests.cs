using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.IdentityModel.Tokens;
using Xunit;

namespace OrderManagement.Tests;

public class OrdersApiTests
    : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public OrdersApiTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetNonExistingOrder_ShouldReturnNotFound()
    {
        // Act
        var response = await _client.GetAsync("/api/orders/999999");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
    
    
    [Fact]
    public async Task CreateOrder_ShouldReturnCreated()
    {
        // // Arrange
        // var client = _factory.CreateClient();

        var request = new
        {
            orderNo = "TEST-20261116-001",
            customerName = "测试客户",
            totalAmount = 199.99m
        };

        // Act
        var response = await _client.PostAsJsonAsync(
            "/api/orders",
            request);

        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var order = await response.Content
            .ReadFromJsonAsync<JsonElement>();

        Assert.Equal(
            "TEST-20261116-001",
            order.GetProperty("orderNo").GetString());

        Assert.Equal(
            "测试客户",
            order.GetProperty("customerName").GetString());

        Assert.Equal(
            199.99m,
            order.GetProperty("totalAmount").GetDecimal());
    }

    [Fact]
    public async Task CreateOrder_WithInvalidData_ShouldReturnBadRequest()
    {
        // Arrange
        // var client = _factory.CreateClient();

        var request = new
        {
            orderNo = "",
            customerName = "测试客户",
            totalAmount = 199.99m
        };

        // Act
        var response = await _client.PostAsJsonAsync(
            "/api/orders",
            request);

        // Assert
        Assert.Equal(
            HttpStatusCode.BadRequest,
            response.StatusCode);

        var result = await response.Content
            .ReadFromJsonAsync<JsonElement>();

        Assert.True(result.TryGetProperty(
            "errors",
            out var errors));

        Assert.True(errors.EnumerateObject().Any());
    }
    
    
    [Fact]
    public async Task GetOrders_WithoutToken_ShouldReturnUnauthorized()
    {
        // Act
        var response = await _client.GetAsync("/api/orders/secure");

        // Assert
        Assert.Equal(
            HttpStatusCode.Unauthorized,
            response.StatusCode);
    }

    
    [Fact]
    public async Task GetAdmin_WithoutAdminRole_ShouldReturnForbidden()
    {
        // Arrange：生成一个有效的普通用户 Token
        var token = GenerateTestToken("User");

        using var request = new HttpRequestMessage(
            HttpMethod.Get,
            "/api/orders/admin");

        request.Headers.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue(
                "Bearer",
                token);

        // Act
        var response = await _client.SendAsync(request);

        // Assert
        Assert.Equal(
            HttpStatusCode.Forbidden,
            response.StatusCode);
    }

    
    private static string GenerateTestToken(string role)
    {
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(
                "TestSecretKey_123456789012345678901234567890"));

        var credentials = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: "OrderManagement.Tests",
            audience: "OrderManagement.Tests",
            claims:
            [
                new Claim(ClaimTypes.NameIdentifier, "test-user"),
                new Claim(ClaimTypes.Name, "test-user"),
                new Claim(ClaimTypes.Role, role)
            ],
            expires: DateTime.UtcNow.AddMinutes(30),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    
    [Fact]
    public async Task GetAdmin_WithAdminRole_ShouldReturnOk()
    {
        // Arrange
        var token = GenerateTestToken("Admin");

        using var request = new HttpRequestMessage(
            HttpMethod.Get,
            "/api/orders/admin");

        request.Headers.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue(
                "Bearer",
                token);

        // Act
        var response = await _client.SendAsync(request);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var result = await response.Content
            .ReadFromJsonAsync<JsonElement>();

        Assert.Equal(
            "test-user",
            result.GetProperty("username").GetString());

        Assert.Equal(
            "Admin",
            result.GetProperty("role").GetString());
    }

    
}