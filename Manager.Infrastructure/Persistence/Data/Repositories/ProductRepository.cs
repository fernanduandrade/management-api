using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using Manager.Domain.Products;

namespace Manager.Infrastructure.Persistence.Data.Repositories;

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
    
    public IQueryable<Product> GetAllPaginated()
    {
        return _repository.GetAll().OrderByDescending(x => x.Created);
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
    
    public void DeleteBulk(List<Guid> ids)
    {
        _repository.DeleteBulk(ids);
    }

    public IQueryable<Product> Get(Expression<Func<Product, bool>> filter = null, bool readOnly = true)
    {
        return _repository.GetAll(filter, readOnly);
    }
}
