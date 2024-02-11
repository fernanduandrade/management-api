using SharedKernel;

namespace Shop.Domain.Orders;

public interface IOrderRepository : IRepository<Order>
{
    void SetEntityStateModified(Order order);
    Task<List<Order>> GetAllPaginated(int pageSize, int pageNumber);
    Task<Order> FindByIdAsync(Guid id);
    void Add(Order order);
    void Update(Order order);
    Task Remove(Guid id);
    Task<List<Order>> GetAllByStatusPaginated(int pageSize, int pageNumber, OrderStatus orderStatus);
}