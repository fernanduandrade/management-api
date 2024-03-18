using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.Orders;

namespace Shop.Infrastructure.Persistence.Data.Configuration;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("orders");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id");

        builder.Property(x => x.ClientName)
            .HasColumnName("client_name");

        builder.Property(x => x.Status)
            .HasColumnName("status")
            .HasConversion(v => v.ToString(),
                v => (OrderStatus)Enum.Parse(typeof(OrderStatus), v));
        
        builder.Property(prop => prop.CreatedBy)
            .HasColumnName("created_by");

        builder.Property(prop => prop.Created)
            .HasColumnName("created");

        builder.Property(prop => prop.LastModified)
            .HasColumnName("last_modified");

        builder.Property(prop => prop.LastModifiedBy)
            .HasColumnName("last_modified_by");
        
        builder.HasMany(o => o.OrderProducts)
            .WithOne(op => op.Order)
            .HasForeignKey(op => op.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}