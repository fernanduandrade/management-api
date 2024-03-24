using SharedKernel;

namespace Shop.Domain.Products;

public interface IProductRepository
{
    Task<bool> IsProductUniqueAsync(string productName);
    void SetEntityStateModified(Product product);
    IQueryable<Product> GetAllPaginated();
    Task<Product> FindByIdAsync(Guid id);
    void Add(Product product);
    void Update(Product product);
    Task Remove(Guid id);
    Task<List<Product>> AutoComplete(string search);
    void DeleteBulk(List<Guid> ids);
}