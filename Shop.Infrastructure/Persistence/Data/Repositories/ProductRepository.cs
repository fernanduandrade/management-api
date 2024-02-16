using Microsoft.EntityFrameworkCore;
using Shop.Domain.Products;

namespace Shop.Infrastructure.Persistence.Data.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
        => (_context) = (context);
    public async Task<bool> IsProductUniqueAsync(string productName)
    {
        var productExists = await _context.Products
            .FirstOrDefaultAsync(product => product.Name.ToUpper() == productName.ToUpper());

        if (productExists is not null)
            return true;

        return false;
    }

    public IQueryable<Product> GetAllPaginated()
    {
        var result = _context.Products
            .AsNoTracking();

        return result;
    }

    public async Task<Product> FindByIdAsync(Guid id)
    {
        var entity = await _context.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

        return entity;
    }

    public void Add(Product product)
        => _context.Products.Add(product);

    public void Update(Product product)
        => _context.Products.Update(product);

    public async Task Remove(Guid id)
    {
        var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
        _context.Products.Remove(product);
    }

    public async Task<List<Product>> AutoComplete(string search)
    {
        var products = await _context.Products.
            Where(x => x.Name.ToLower().Contains(search.ToLower()))
            .ToListAsync();
        
        return products;
    }

    public virtual void SetEntityStateModified(Product entity)
    {
        _context.Products.Entry(entity).State = EntityState.Modified;
    }
}
