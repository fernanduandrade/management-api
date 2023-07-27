using Microsoft.EntityFrameworkCore;
using Shop.Application.Product.Interfaces;
using Shop.Domain.Entities;

namespace Shop.Infrastructure.Persistence.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _appContext;

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
            .FirstOrDefaultAsync(p => p.Id == id);

        if (product is not null) return product;
        return null;
    }
}
