using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Product.Interfaces;
using Shop.Domain.Entities;

namespace Shop.Infrastructure.Persistence.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _appContext;
    private readonly IMapper _mapper;

    public ProductRepository(AppDbContext appContext)
    {
      _appContext = appContext;
    }
    public async Task<bool> IsProductUniqueAsync(string productName)
    {
        var productExists = await _appContext.Products
            .FirstOrDefaultAsync(product => product.Name.ToUpper() == productName.ToUpper());
    
        if(productExists is not null) {
          return true;
        }

        return false;
    }

    public async Task<Product> FindByIdAsync(long id)
    {
        var product = await _appContext.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);

        return product;
    }

    public async Task<List<Product>> GetAllPaginated(int pageSize, int pageNumber)
    {
        var result = await _appContext.Products
            .AsNoTracking()
            .Take(pageSize)
            .Skip(pageNumber)
            .ToListAsync();

        return result;
    }

    public virtual void SetEntityStateModified(Product entity)
    {
        _appContext.Products.Entry(entity).State = EntityState.Modified;
    }
}
