using SharedKernel;
using Shop.Domain.Entities;

namespace Shop.Domain.Events;

public class SaleCreateEvent : IDomainEvent
{
    public SalesHistory Item { get; set; }

    public SaleCreateEvent(SalesHistory salesHistory)
        => (Item) = (salesHistory);
}