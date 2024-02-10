using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.OrderProducts;

namespace Shop.Infrastructure.Persistence.Data.Configuration;

public class OrderProductConfiguration : IEntityTypeConfiguration<OrderProduct>
{
    public void Configure(EntityTypeBuilder<OrderProduct> builder)
    {
        builder.ToTable("order_products");
        builder.HasKey(x => x.Id);
        
        builder.Property(prop => prop.Id)
            .HasColumnName("id");

        builder.Property(x => x.OrderId)
            .HasColumnName("product_id");
        
        builder.Property(x => x.ProductId)
            .HasColumnName("order_id");
        
        builder.Property(prop => prop.CreatedBy)
            .HasColumnName("created_by");

        builder.Property(prop => prop.Created)
            .HasColumnName("created");

        builder.Property(prop => prop.LastModified)
            .HasColumnName("last_modified");

        builder.Property(prop => prop.LastModifiedBy)
            .HasColumnName("last_modified_by");
        
        builder.HasOne(op => op.Order)
            .WithMany(o => o.OrderProducts)
            .HasForeignKey(op => op.OrderId);

        builder.HasOne(op => op.Product)
            .WithMany(p => p.OrderProducts)
            .HasForeignKey(op => op.ProductId);
    }
}