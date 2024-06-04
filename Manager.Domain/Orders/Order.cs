using SharedKernel;
using Manager.Domain.OrderProducts;
using Manager.Domain.SalesHistory;
using Manager.Domain.SalesHistory.Events;

namespace Manager.Domain.Orders;

public class Order : AuditableEntity, IAggregateRoot
{
    public OrderStatus Status { get; private set; }
    public string ClientName { get; private set; }
    
    public List<OrderProduct> OrderProducts { get; private set; }

    public static Order Create(OrderStatus orderStatus, string clientName)
    {
        Order order = new()
        {
            Id = new Guid(),
            Status = orderStatus,
            ClientName = clientName
        };

        return order;
    }

    public void DispatchProductsSold(List<OrderProduct> products, string clientName, PaymentType paymentType)
    {
        Raise(new CreateBulkSaleEvent(products, clientName, paymentType));
    }

    public void CloseOrder()
    {
        Status = OrderStatus.FECHADO;
    }
}