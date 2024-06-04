using System.Linq.Expressions;

namespace Manager.Domain.Orders;

public interface IOrderRepository
{
    Task<Order> FindByIdAsync(Guid id);
    void Add(Order order);
    Task Remove(Guid id);
    IQueryable<Order> GetAllByStatus(OrderStatus orderStatus);
    void DeleteBulk(List<Guid> ids);
    IQueryable<Order> Get(Expression<Func<Order, bool>> filter = null, bool readOnly = true);
}