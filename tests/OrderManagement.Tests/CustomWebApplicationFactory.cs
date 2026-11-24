using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using OrderManagement.Api.Data;

namespace OrderManagement.Tests;

public class CustomWebApplicationFactory
    : WebApplicationFactory<Program>
{
    private readonly string _databaseName =
        $"OrderManagement_IntegrationTests_{Guid.NewGuid()}";

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");

        // JWT 测试配置
        builder.UseSetting("Jwt:Issuer", "OrderManagement.Tests");
        builder.UseSetting("Jwt:Audience", "OrderManagement.Tests");
        builder.UseSetting(
            "Jwt:SecretKey",
            "TestSecretKey_123456789012345678901234567890");
        builder.UseSetting("Jwt:ExpirationMinutes", "30");

        builder.ConfigureTestServices(services =>
        {
            // Integration tests do not depend on external message brokers.
            services.RemoveAll<IHostedService>();

            // 移除原来的 DbContext 注册
            services.RemoveAll<ApplicationDbContext>();
            services.RemoveAll<DbContextOptions<ApplicationDbContext>>();

            // 关键：移除原来保存的 PostgreSQL Provider 配置
            services.RemoveAll<
                IDbContextOptionsConfiguration<ApplicationDbContext>>();

            // 注册 InMemory Provider
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseInMemoryDatabase(_databaseName);
            });

            // 使用内存缓存替代 Redis
            services.RemoveAll<IDistributedCache>();
            services.AddDistributedMemoryCache();
        });
    }
}