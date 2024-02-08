using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.Products;

namespace Shop.Infrastructure.Persistence.Data.Configuration;

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
    }
}