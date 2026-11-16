using Microsoft.Extensions.Diagnostics.HealthChecks;
using OrderManagement.Api.Data;

namespace OrderManagement.Api.HealthChecks;

public class PostgreSqlHealthCheck(ApplicationDbContext dbContext) : IHealthCheck
{
    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {

        try
        {
            var canContent = await dbContext.Database.CanConnectAsync(cancellationToken);
        
            return canContent? HealthCheckResult.Healthy("PostgreSQL is healthy.") : HealthCheckResult.Unhealthy("PostgreSQL is unhealthy.");

        }
        catch (Exception e)
        {
           return HealthCheckResult.Unhealthy("PostgreSQL is unhealthy.", e);
        }
    }
}