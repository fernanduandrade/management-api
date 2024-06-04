namespace Shop.Domain.Orders;

public interface IOrderRepository
{
    Task<Order> FindByIdAsync(Guid id);
    void Add(Order order);
    void Update(Order order);
    Task Remove(Guid id);
    IQueryable<Order> GetAllByStatus(OrderStatus orderStatus);
    int GetTotalOrders();
    int GetTotalClosed();
    int GetTotalOpen();
    void DeleteBulk(List<Guid> ids);
}