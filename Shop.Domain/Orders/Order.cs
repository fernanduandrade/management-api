using SharedKernel;
using Shop.Domain.OrderProducts;

namespace Shop.Domain.Orders;

public class Order : AuditableEntity, IAggregateRoot
{
    public OrderStatus OrderStatus { get; private set; }
    public string ClientName { get; private set; }
    
    public List<OrderProduct> OrderProducts { get; private set; }

    public static Order Create(OrderStatus orderStatus, string clientName)
    {
        Order order = new()
        {
            Id = new Guid(),
            OrderStatus = orderStatus,
            ClientName = clientName
        };

        return order;
    }
}