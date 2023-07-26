namespace Shop.Application.Product.Interfaces;

public interface IProductRepository
{
  Task<bool> IsProductUniqueAsync(string productName);
}
