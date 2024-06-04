using Microsoft.EntityFrameworkCore;
using SharedKernel;
using Shop.Domain.OrderProducts;

namespace Shop.Infrastructure.Persistence.Data.Repositories;

public class OrderProductRepository : IOrderProductRepository
{
    private readonly AppDbContext _context;
    private readonly DbSet<OrderProduct> _dbSet;
    private readonly IRepository<OrderProduct> _repository;


    public OrderProductRepository(AppDbContext context, IRepository<OrderProduct> repository)
    {
        _context = context;
        _dbSet = _context.Set<OrderProduct>();
        _repository = repository;
    }

    public async Task<OrderProduct> FindByIdAsync(Guid id)
        => await _repository.FindByIdAsync(id);

    public void Add(OrderProduct orderProduct)
        => _repository.Add(orderProduct);

    public void Update(OrderProduct orderProduct)
        => _repository.Update(orderProduct);

    public async Task FindByIdAndRemove(Guid id)
    {
        var orderProduct = await _dbSet
            .FirstOrDefaultAsync(x => x.Id == id);
        _dbSet.Remove(orderProduct);
    }
    
    public void Remove(OrderProduct orderProduct)
    {
        _dbSet.Remove(orderProduct);
    }
    
    public async Task<OrderProduct> OrderProductExist(Guid productId, Guid orderId)
    {
        var orderProduct = await _dbSet
            .FirstOrDefaultAsync(x => x.OrderId == orderId && x.ProductId == productId);

        return orderProduct;
    }
}