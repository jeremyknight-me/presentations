using DomainDriven.Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainDriven.Persistence.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasIndex(p => p.Id);

        builder.Property(p => p.Id)
            .HasConversion(
                productId => productId.Value,
                value => new ProductId(value)
            );

        builder.Property(p => p.Sku)
            .HasConversion(
                sku => sku.Value,
                value => Sku.Create(value)!
            );

        builder.OwnsOne(p => p.Price,
            priceBuilder =>
            {
                priceBuilder.Property(m => m.Currency)
                    .HasMaxLength(3)
                    .IsUnicode();
                priceBuilder.Property(m => m.Amount)
                    .HasPrecision(18, 5);
            });
    }
}
