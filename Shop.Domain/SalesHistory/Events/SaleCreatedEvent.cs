using SharedKernel;

namespace Shop.Domain.SalesHistory.Events;

public record SaleCreatedEvent(Guid ProductId, int Quantity) : IDomainEvent;