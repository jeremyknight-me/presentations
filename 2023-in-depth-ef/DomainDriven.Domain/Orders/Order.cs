using DomainDriven.Domain.Customers;

namespace DomainDriven.Domain.Orders;

public class Order
{
    private readonly List<LineItem> lineItems = new();

    private Order()
    {
    }

    public OrderId Id { get; private set; }
    public CustomerId CustomerId { get; private set; }
    public IReadOnlyList<LineItem> LineItems => this.lineItems.AsReadOnly();

    public static Order Create(CustomerId customerId)
    {
        var order = new Order
        {
            Id = new OrderId(Guid.NewGuid()),
            CustomerId = customerId
        };
        return order;
    }
}
