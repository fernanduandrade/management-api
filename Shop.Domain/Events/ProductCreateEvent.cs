namespace Shop.Domain.Events;

public class ProductCreateEvent : BaseEvent
{
    public Product Item { get; set; }

    public ProductCreateEvent(Product product)
        => (Item) = (product);
}