using Microsoft.EntityFrameworkCore;
using OrderManagement.Domain.Aggregates;

namespace OrderManagement.Infrastructure.Persistence;

public class OrderManagementDbContext:DbContext
{
    public OrderManagementDbContext(
        DbContextOptions<OrderManagementDbContext> options)
        : base(options)
    {
        
    }
    
    public DbSet<Order> Orders => Set<Order>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(OrderManagementDbContext).Assembly);
    }
    
}