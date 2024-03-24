using Microsoft.EntityFrameworkCore;
using SharedKernel;
using Shop.Domain.Products;

namespace Shop.Infrastructure.Persistence.Data.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;
    private readonly IRepository<Product> _repository;
    private readonly DbSet<Product> _dbSet;


    public ProductRepository(AppDbContext context, IRepository<Product> repository)
    {
        _context = context;
        _dbSet = _context.Set<Product>();
        _repository = repository;
    }

    public async Task<bool> IsProductUniqueAsync(string productName)
    {
        var productExists = await _dbSet
            .FirstOrDefaultAsync(product => product.Name.ToUpper() == productName.ToUpper());

        if (productExists is not null)
            return true;

        return false;
    }

    public IQueryable<Product> GetAllPaginated()
    {
        return _repository.GetAll();
    }

    public async Task<Product> FindByIdAsync(Guid id)
    {
        return await _repository.FindByIdAsync(id);
    }

    public void Add(Product product)
        => _repository.Add(product);

    public void Update(Product product)
        => _repository.Update(product);

    public async Task Remove(Guid id)
    {
        await _repository.Remove(id);
    }

    public async Task<List<Product>> AutoComplete(string search)
    {
        var products = await _dbSet.
            Where(x => x.Name.ToLower().Contains(search.ToLower()))
            .ToListAsync();
        
        return products;
    }

    public virtual void SetEntityStateModified(Product entity)
    {
        _repository.SetEntityStateModified(entity);
    }

    public void DeleteBulk(List<Guid> ids)
    {
        _repository.DeleteBulk(ids);
    }
}
