namespace Shop.Domain.Events;

public class ClientCreateEvent : BaseEvent
{
    public Client Item { get; set; }

    public ClientCreateEvent(Client product)
        => (Item) = (product);
}