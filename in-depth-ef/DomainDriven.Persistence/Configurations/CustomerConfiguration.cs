using DomainDriven.Domain.Customers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainDriven.Persistence.Configurations;

internal class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .HasConversion(
                customerId => customerId.Value,
                value => new CustomerId(value)
            );

        builder.Property(c => c.Name)
            .HasMaxLength(100)
            .IsUnicode()
            .IsRequired();

        builder.Property(c => c.Email)
            .HasMaxLength(200)
            .IsUnicode()
            .IsRequired();

        builder.HasIndex(c => c.Email)
            .IsUnique();
    }
}
