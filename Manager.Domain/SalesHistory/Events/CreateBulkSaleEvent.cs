using SharedKernel;
using Manager.Domain.OrderProducts;

namespace Manager.Domain.SalesHistory.Events;

public record CreateBulkSaleEvent(List<OrderProduct> products, string clientName, PaymentType paymentType) : IDomainEvent;