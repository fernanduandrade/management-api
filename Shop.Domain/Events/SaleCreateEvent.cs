namespace Shop.Domain.Events;

public class SaleCreateEvent : BaseEvent
{
    public Sale Item { get; set; }

    public SaleCreateEvent(Sale sale)
        => (Item) = (sale);
}