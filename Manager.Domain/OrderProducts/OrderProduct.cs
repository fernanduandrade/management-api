using SharedKernel;
using Manager.Domain.Orders;
using Manager.Domain.Products;

namespace Manager.Domain.OrderProducts;

public class OrderProduct : AuditableEntity, IAggregateRoot
{
    public Product Product { get; private set; }
    public Guid ProductId { get; private set; }
    public Order Order { get; private set; }
    public Guid OrderId { get; private set; }
    public int Quantity { get; private set; }

    public static OrderProduct Create(Guid productId, Guid orderId)
    {
        OrderProduct orderProduct = new()
        {
            Id = new Guid(),
            ProductId = productId,
            OrderId = orderId,
            Quantity = 1
        };

        return orderProduct;
    }

    public void IncrementQuantity()
    {
        Quantity += 1;
    }
    
    public void DecrementQuantity()
    {
        Quantity += -1;
    }
}