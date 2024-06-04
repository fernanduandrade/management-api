using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Manager.Domain.Products;

namespace Manager.Infrastructure.Persistence.Data.Configuration;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("products");
        builder.HasKey(prop => prop.Id);
        
        builder.Property(prop => prop.Id)
            .HasColumnName("id");

        builder.Property(prop => prop.Description)
            .HasColumnName("description");
        
        builder.Property(prop => prop.Name)
            .HasColumnName("name");
        
        builder.Property(prop => prop.Price)
            .HasColumnName("price");
        
        builder.Property(prop => prop.Quantity)
            .HasColumnName("quantity");
        
        builder.Property(prop => prop.IsAvaliable)
            .HasColumnName("is_available");
        
        builder.Property(prop => prop.CreatedBy)
            .HasColumnName("created_by");

        builder.Property(prop => prop.Created)
            .HasColumnName("created");

        builder.Property(prop => prop.LastModified)
            .HasColumnName("last_modified");

        builder.Property(prop => prop.LastModifiedBy)
            .HasColumnName("last_modified_by");
        
        builder.HasMany(o => o.OrderProducts)
            .WithOne(op => op.Product)
            .HasForeignKey(op => op.ProductId)
            .OnDelete(DeleteBehavior.Cascade);
    
        builder.HasMany(o => o.SaleHistorys)
            .WithOne(op => op.Product)
            .HasForeignKey(op => op.ProductId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}