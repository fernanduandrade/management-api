using Entities = Shop.Domain.Entities;
namespace Shop.Application.Product.Interfaces;

public interface IProductRepository
{
  Task<bool> IsProductUniqueAsync(string productName);
  void SetEntityStateModified(Entities.Product entity);
  Task<List<Entities.Product>> GetAllPaginated(int pageSize, int pageNumber);
  Task<Entities.Product> FindByIdAsync(long id);
}
