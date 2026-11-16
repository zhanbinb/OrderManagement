using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace OrderManagement.Api.HealthChecks;

public class RedisHealthCheck(
    IDistributedCache cache) : IHealthCheck
{
    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        var cacheKey = $"health-check:{Guid.NewGuid()}";
        const string cacheValue = "Redis is available";

        try
        {
            // 写入测试数据
            await cache.SetStringAsync(
                cacheKey,
                cacheValue,
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow =
                        TimeSpan.FromSeconds(30)
                },
                cancellationToken);

            // 读取测试数据
            var value = await cache.GetStringAsync(
                cacheKey,
                cancellationToken);

            if (value != cacheValue)
            {
                return HealthCheckResult.Unhealthy(
                    "Redis read/write check failed");
            }

            return HealthCheckResult.Healthy(
                "Redis is available");
        }
        catch
        {
            return HealthCheckResult.Unhealthy(
                "Redis health check failed");
        }
        finally
        {
            try
            {
                await cache.RemoveAsync(cacheKey, cancellationToken);
            }
            catch
            {
                // 健康检查清理失败不覆盖原检查结果
            }
        }
    }
}