using System.Linq.Expressions;
using SharedKernel;

namespace Manager.Domain.Products;

public interface IProductRepository
{
    IQueryable<Product> GetAllPaginated();
    Task<Product> FindByIdAsync(Guid id);
    void Add(Product product);
    void Update(Product product);
    Task Remove(Guid id);
    void DeleteBulk(List<Guid> ids);
    IQueryable<Product> Get(Expression<Func<Product, bool>> filter = null, bool readOnly = true);
}