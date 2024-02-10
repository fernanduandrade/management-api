using Microsoft.EntityFrameworkCore;
using Shop.Domain.OrderProducts;

namespace Shop.Infrastructure.Persistence.Data.Repositories;

public class OrderProductRepository : IOrderProductRepository
{
    private readonly AppDbContext _context;

    public OrderProductRepository(AppDbContext context)
    {
        _context = context;
    }
    public void SetEntityStateModified(OrderProduct orderProduct)
    {
        _context.OrderProducts.Entry(orderProduct).State = EntityState.Modified;
    }

    public async Task<List<OrderProduct>> GetAllPaginated(int pageSize, int pageNumber)
    {
        var orderProducts = await _context.OrderProducts.AsNoTracking()
            .Take(pageSize)
            .Skip(pageNumber)
            .ToListAsync();

        return orderProducts;

    }

    public async Task<OrderProduct> FindByIdAsync(Guid id)
        => await _context.OrderProducts
            .FirstOrDefaultAsync(x => x.Id == id);

    public void Add(OrderProduct orderProduct)
        => _context.OrderProducts.Add(orderProduct);

    public void Update(OrderProduct orderProduct)
        => _context.OrderProducts.Update(orderProduct);

    public async Task Remove(Guid id)
    {
        var orderProduct = await _context.OrderProducts
            .FirstOrDefaultAsync(x => x.Id == id);
        _context.OrderProducts.Remove(orderProduct);
    }
}