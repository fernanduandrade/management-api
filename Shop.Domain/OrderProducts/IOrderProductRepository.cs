using SharedKernel;

namespace Shop.Domain.OrderProducts;

public interface IOrderProductRepository : IRepository<OrderProduct>
{
    void SetEntityStateModified(OrderProduct orderProduct);
    Task<List<OrderProduct>> GetAllPaginated(int pageSize, int pageNumber);
    Task<OrderProduct> FindByIdAsync(Guid id);
    void Add(OrderProduct orderProduct);
    void Update(OrderProduct orderProduct);
    Task Remove(Guid id);    
}