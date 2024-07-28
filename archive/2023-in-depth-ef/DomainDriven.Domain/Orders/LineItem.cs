using DomainDriven.Domain.Products;

namespace DomainDriven.Domain.Orders;

public class LineItem
{
    private LineItem()
    {
    }

    public LineItemId Id { get; private set; }
    public OrderId OrderId { get; private set; }
    public ProductId ProductId { get; private set; }
    public Money Price { get; private set; }

    public static LineItem Create(LineItemId id, OrderId orderId, ProductId productId, Money price)
        => new()
        {
            Id = id,
            OrderId = orderId,
            ProductId = productId,
            Price = price
        };
}
