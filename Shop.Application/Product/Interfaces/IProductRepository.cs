namespace Shop.Application.Product.Interfaces;

public interface IProductRepository
{
  Task<bool> IsProductUniqueAsync(string productName);
  Task<Domain.Entities.Product> FindByIdAsync(long id);
}
