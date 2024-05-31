using DomainDriven.Domain.Orders;
using DomainDriven.Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainDriven.Persistence.Configurations;

internal class LineItemConfiguration : IEntityTypeConfiguration<LineItem>
{
    public void Configure(EntityTypeBuilder<LineItem> builder)
    {
        builder.HasKey(li => li.Id);

        builder.Property(li => li.Id)
            .HasConversion(
                lineItemId => lineItemId.Value,
                value => new LineItemId(value)
            );
        ;

        builder.HasOne<Product>()
            .WithMany()
            .HasForeignKey(li => li.ProductId);

        builder.OwnsOne(li => li.Price,
            pricebuilder =>
            {
                pricebuilder.Property(m => m.Currency)
                    .HasMaxLength(3)
                    .IsUnicode();
                pricebuilder.Property(m => m.Amount)
                    .HasPrecision(18, 5);
            });
    }
}
