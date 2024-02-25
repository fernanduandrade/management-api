using Microsoft.EntityFrameworkCore;
using Shop.Domain.Orders;

namespace Shop.Infrastructure.Persistence.Data.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _context;

    public OrderRepository(AppDbContext context)
    {
        _context = context;
    }
    public void SetEntityStateModified(Order order)
    {
        _context.Orders.Entry(order).State = EntityState.Modified;
    }

    public async Task<List<Order>> GetAllPaginated(int pageSize, int pageNumber)
    {
        var orders = await _context.Orders.AsNoTracking()
            .Take(pageSize)
            .Skip(pageNumber)
            .ToListAsync();

        return orders;
    }

    public async Task<Order> FindByIdAsync(Guid id)
    {
        var order = await _context.Orders
            .Include(x => x.OrderProducts)
            .ThenInclude(x => x.Product)
            .FirstOrDefaultAsync(x => x.Id == id);
        return order;
    }

    public void Add(Order order)
    {
        _context.Orders.Add(order);
    }

    public void Update(Order order)
    {
        _context.Orders.Update(order);
    }

    public async Task Remove(Guid id)
    {
        var order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == id);
        _context.Orders.Remove(order);
    }

    public IQueryable<Order> GetAllByStatus(OrderStatus orderStatus)
    {
        if(orderStatus == OrderStatus.TODOS){
            return _context.Orders.AsNoTracking();
        }

        var orders = _context.Orders.AsNoTracking()
            .Where(x => x.Status == orderStatus);

        return orders;
    }
}