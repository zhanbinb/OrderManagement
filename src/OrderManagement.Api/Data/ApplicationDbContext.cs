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
}
