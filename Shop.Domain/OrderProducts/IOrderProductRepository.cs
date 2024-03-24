using SharedKernel;

namespace Shop.Domain.OrderProducts;

public interface IOrderProductRepository
{
    void SetEntityStateModified(OrderProduct orderProduct);
    Task<OrderProduct> FindByIdAsync(Guid id);
    void Add(OrderProduct orderProduct);
    void Update(OrderProduct orderProduct);
    Task FindByIdAndRemove(Guid id);
    Task<OrderProduct> OrderProductExist(Guid productId, Guid orderId);
    void Remove(OrderProduct orderProduct);
}