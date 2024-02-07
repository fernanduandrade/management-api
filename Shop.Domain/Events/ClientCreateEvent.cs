using SharedKernel;
using Shop.Domain.Entities;

namespace Shop.Domain.Events;

public class ClientCreateEvent : IDomainEvent
{
    public Client Item { get; set; }

    public ClientCreateEvent(Client client)
        => (Item) = (client);
}