using SharedKernel;
using Shop.Domain.OrderProducts;
using Shop.Domain.SalesHistory;
using Shop.Domain.SalesHistory.Events;

namespace Shop.Domain.Orders;

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