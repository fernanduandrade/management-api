using MediatR;
using Shop.Application.Common.Interfaces;
using Shop.Application.Product.Interfaces;
using Shop.Domain.Events;
using Shop.Domain.Errors;
using Entities = Shop.Domain.Entities;
namespace Namespace;
public class CreateSaleEventHandler : INotificationHandler<SaleCreateEvent>
{
  private readonly IProductRepository _productRepository;
  private readonly IAppDbContext _context;

  public CreateSaleEventHandler(IProductRepository productRepository, IAppDbContext context)
   => (_productRepository, _context) = (productRepository, context);
    public async Task Handle(SaleCreateEvent notification, CancellationToken cancellationToken)
    {
        var sale = notification.Item;
        long productId = sale.ProductFk;
        var product = await _productRepository.FindByIdAsync(productId);

        if(product is null) {
            throw new InvalidProductException("Produto não encontrado"); 
        }
        product.Quantity = product.Quantity - sale.Quantity;

        try 
        {
            var newProduct = new Entities.Product()
            {
                Id = product.Id,
                Description = product.Description,
                Name = product.Name,
                Price = product.Price,
                Quantity = product.Quantity,
                
            };

            _context.Products.Update(newProduct);
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch(Exception ex)
        {
            Console.WriteLine("asdasdsad");
        }
        
    }
}
