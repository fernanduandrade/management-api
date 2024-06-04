using System.Linq.Expressions;
namespace Manager.Domain.OrderProducts;

public interface IOrderProductRepository
{
    void Add(OrderProduct orderProduct);
    void Remove(OrderProduct orderProduct);
    IQueryable<OrderProduct> Get(Expression<Func<OrderProduct, bool>> filter = null, bool readOnly = true);
}