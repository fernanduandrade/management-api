using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.Entities;

namespace Shop.Infrastructure.Persistence.Configuration;

public class SaleConfiguration : IEntityTypeConfiguration<Sale>
{
    public void Configure(EntityTypeBuilder<Sale> builder)
    {
        builder.ToTable("sales");
        builder.HasKey(prop => prop.Id);
        
        builder.Property(prop => prop.Id)
            .HasColumnName("id");

        builder.Property(prop => prop.Quantity)
            .HasColumnName("quantity");
        
        builder.Property(prop => prop.ClientName)
            .HasColumnName("client_name");

        builder.Property(prop => prop.ProductName)
            .HasColumnName("product_name");

        builder.Property(prop => prop.SaleDate)
            .HasColumnName("sale_date");

        builder.Property(prop => prop.PricePerUnit)
            .HasColumnName("price_per_unit");
        
        builder.Property(prop => prop.TotalPrice)
            .HasColumnName("total_price");
        
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