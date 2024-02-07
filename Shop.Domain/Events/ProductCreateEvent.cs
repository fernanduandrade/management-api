using SharedKernel;
using Shop.Domain.Entities;

namespace Shop.Domain.Events;

public class ProductCreateEvent : IDomainEvent
{
    public Product Item { get; set; }

    public ProductCreateEvent(Product product)
        => (Item) = (product);
}