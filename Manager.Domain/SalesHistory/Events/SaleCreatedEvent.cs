using SharedKernel;

namespace Manager.Domain.SalesHistory.Events;

public record SaleCreatedEvent(Guid ProductId, int Quantity) : IDomainEvent;