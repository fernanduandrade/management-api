using SharedKernel;
using Shop.Domain.Orders;
using Shop.Domain.Products;

namespace Shop.Domain.OrderProducts;

public class OrderProduct : AuditableEntity, IAggregateRoot
{
    public Product Product { get; private set; }
    public Guid ProductId { get; private set; }
    public Order Order { get; private set; }
    public Guid OrderId { get; private set; }

    public static OrderProduct Create(Guid productId, Guid orderId)
    {
        OrderProduct orderProduct = new()
        {
            Id = new Guid(),
            ProductId = productId,
            OrderId = orderId
        };

        return orderProduct;
    }
}