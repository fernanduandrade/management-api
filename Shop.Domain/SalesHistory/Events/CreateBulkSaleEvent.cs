using SharedKernel;
using Shop.Domain.OrderProducts;

namespace Shop.Domain.SalesHistory.Events;

public record CreateBulkSaleEvent(List<OrderProduct> products, string clientName, PaymentType paymentType) : IDomainEvent;