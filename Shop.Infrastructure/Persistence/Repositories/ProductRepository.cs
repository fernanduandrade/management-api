using Microsoft.EntityFrameworkCore;
using Shop.Application.Product.Interfaces;
using Shop.Domain.Entities;

namespace Shop.Infrastructure.Persistence.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
        => (_context) = (context);
    public async Task<bool> IsProductUniqueAsync(string productName)
    {
        var productExists = await _context.Products
            .FirstOrDefaultAsync(product => product.Name.ToUpper() == productName.ToUpper());
    
        if(productExists is not null) {
          return true;
        }

        return false;
    }

    public async Task<List<Product>> GetAllPaginated(int pageSize, int pageNumber)
    {
        var result = await _context.Products
            .AsNoTracking()
            .Take(pageSize)
            .Skip(pageNumber)
            .ToListAsync();

        return result;
    }

    public async Task<Product> FindByIdAsync(long id)
    {
        var entity = await _context.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

        return entity;
    }

    public virtual void SetEntityStateModified(Product entity)
    {
        _context.Products.Entry(entity).State = EntityState.Modified;
    }
}
