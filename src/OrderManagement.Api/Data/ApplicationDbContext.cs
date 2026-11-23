using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using OrderManagement.Api.Entities;

namespace OrderManagement.Api.Data;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Order> Orders { get; set; }
    
    public DbSet<ProcessedMessage> ProcessedMessages => Set<ProcessedMessage>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    
        modelBuilder.Entity<Order>()
            .HasIndex(x => x.OrderNo)
            .IsUnique();
        
        modelBuilder.Entity<Order>()
            .Property(o => o.Version)
            .IsRowVersion();
        
        modelBuilder.Entity<ProcessedMessage>(entity =>
        {
            entity.HasKey(x => x.Id);

            entity.HasIndex(x => x.MessageId)
                .IsUnique();
        });
    }
}
