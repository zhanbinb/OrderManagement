using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderManagement.Domain.Aggregates;

namespace OrderManagement.Infrastructure.Persistence.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Status)
            .IsRequired();

        builder.Ignore(x => x.TotalAmount);

        builder.OwnsMany(x => x.Items, item =>
        {
            item.ToTable("OrderItems");

            item.WithOwner()
                .HasForeignKey("OrderId");

            item.HasKey(x => x.Id);

            item.Property(x => x.Id)
                .ValueGeneratedNever();

            item.Property(x => x.ProductName)
                .HasMaxLength(200)
                .IsRequired();

            item.Property(x => x.ProductId)
                .IsRequired();

            item.Property(x => x.Quantity)
                .IsRequired();

            item.OwnsOne(x => x.UnitPrice, money =>
            {
                money.Property(x => x.Amount)
                    .HasPrecision(18, 2)
                    .HasColumnName("UnitPrice");

                money.Property(x => x.Currency)
                    .HasMaxLength(3)
                    .HasColumnName("Currency");
            });
        });

        builder.Navigation(x => x.Items)
            .UsePropertyAccessMode(
                Microsoft.EntityFrameworkCore.PropertyAccessMode.Field);
    }
}