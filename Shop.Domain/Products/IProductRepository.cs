using SharedKernel;

namespace Shop.Domain.Products;

public interface IProductRepository : IRepository<Product>
{
    Task<bool> IsProductUniqueAsync(string productName);
    void SetEntityStateModified(Product product);
    Task<List<Product>> GetAllPaginated(int pageSize, int pageNumber);
    Task<Product> FindByIdAsync(Guid id);
    void Add(Product product);
    void Update(Product product);
    Task Remove(Guid id);
}