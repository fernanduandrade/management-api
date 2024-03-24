using Microsoft.EntityFrameworkCore;
using SharedKernel;
using Shop.Domain.Orders;

namespace Shop.Infrastructure.Persistence.Data.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _context;
    private readonly DbSet<Order> _dbSet;
    private readonly IRepository<Order> _repository;

    public OrderRepository(AppDbContext context, IRepository<Order> repository)
    {
        _context = context;
        _dbSet = _context.Set<Order>();
        _repository = repository;
    }
    public void SetEntityStateModified(Order order)
    {
        _repository.SetEntityStateModified(order);
    }

    public async Task<Order> FindByIdAsync(Guid id)
    {
        var order = await _dbSet
            .Include(x => x.OrderProducts)
            .ThenInclude(x => x.Product)
            .FirstOrDefaultAsync(x => x.Id == id);
        return order;
    }

    public void Add(Order order)
    {
        _repository.Add(order);
    }

    public void Update(Order order)
    {
        _repository.Update(order);
    }

    public async Task Remove(Guid id)
    {
        await _repository.Remove(id);
    }

    public IQueryable<Order> GetAllByStatus(OrderStatus orderStatus)
    {
        if(orderStatus == OrderStatus.TODOS){
            return _dbSet.AsNoTracking().OrderByDescending(x => x.Created);
        }

        var orders = _dbSet.AsNoTracking()
            .Where(x => x.Status == orderStatus).OrderByDescending(x => x.Created);

        return orders;
    }
    
    public int GetTotalOrders()
     => _dbSet.AsNoTracking().ToList().Count;
    
    public int GetTotalClosed()
        => _dbSet.AsNoTracking().Where(x => x.Status == OrderStatus.FECHADO).Count();
    
    public int GetTotalOpen()
        => _dbSet.AsNoTracking().Where(x => x.Status == OrderStatus.ABERTO).Count();

    public void DeleteBulk(List<Guid> ids)
    {
        _repository.DeleteBulk(ids);
    } 
}