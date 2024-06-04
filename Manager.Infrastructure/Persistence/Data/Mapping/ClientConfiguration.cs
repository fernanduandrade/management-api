using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Manager.Domain.Clients;

namespace Manager.Infrastructure.Persistence.Data.Configuration;

public class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.ToTable("clients");
        builder.HasKey(prop => prop.Id);
        
        builder.Property(prop => prop.Id)
            .HasColumnName("id");
        
        builder.Property(prop => prop.Name)
            .HasColumnName("name");
        
        builder.Property(prop => prop.Credit)
            .HasColumnName("credit");
        
        builder.Property(prop => prop.Debt)
            .HasColumnName("debt");
        
        builder.Property(prop => prop.Phone)
            .HasColumnName("phone");
        
        builder.Property(prop => prop.LastName)
            .HasColumnName("last_name");
        
        builder.Property(prop => prop.IsActive)
            .HasColumnName("is_active");

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