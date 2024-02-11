using SharedKernel;
using Shop.Domain.OrderProducts;

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
}