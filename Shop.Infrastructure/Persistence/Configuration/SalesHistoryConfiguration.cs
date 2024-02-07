using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.Entities;

namespace Shop.Infrastructure.Persistence.Configuration;

public class SaleConfiguration : IEntityTypeConfiguration<SalesHistory>
{
    public void Configure(EntityTypeBuilder<SalesHistory> builder)
    {
        builder.ToTable("sales_history");
        builder.HasKey(prop => prop.Id);
        
        builder.Property(prop => prop.Id)
            .HasColumnName("id");

        builder.Property(prop => prop.Quantity)
            .HasColumnName("quantity");
        
        builder.Property(prop => prop.ClientId)
            .HasColumnName("client_fk");

        builder.Property(prop => prop.ProductId)
            .HasColumnName("product_fk");

        builder.Property(prop => prop.Date)
            .HasColumnName("date");

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
        
        builder.HasOne(prop => prop.Product)
            .WithMany()
            .HasForeignKey(prop => prop.ProductId)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasOne(prop => prop.Client)
            .WithMany()
            .HasForeignKey(prop => prop.ClientId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}