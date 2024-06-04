using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Manager.Domain.SalesHistory;

namespace Manager.Infrastructure.Persistence.Data.Configuration;

public class SaleConfiguration : IEntityTypeConfiguration<SaleHistory>
{
    public void Configure(EntityTypeBuilder<SaleHistory> builder)
    {
        builder.ToTable("sales_history");
        builder.HasKey(prop => prop.Id);
        
        builder.Property(prop => prop.Id)
            .HasColumnName("id");

        builder.Property(prop => prop.Quantity)
            .HasColumnName("quantity");
        
        builder.Property(prop => prop.ClientName)
            .HasColumnName("client_name");

        builder.Property(prop => prop.ProductId)
            .HasColumnName("product_id");

        builder.Property(prop => prop.Date)
            .HasColumnName("date")
            .HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

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

        builder.Property(prop => prop.PaymentType)
            .HasColumnName("payment_type")
            .HasConversion(v => v.ToString(),
                v => (PaymentType)Enum.Parse(typeof(PaymentType), v));
        
        builder.HasOne(prop => prop.Product)
            .WithMany(op => op.SaleHistorys)
            .HasForeignKey(prop => prop.ProductId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}