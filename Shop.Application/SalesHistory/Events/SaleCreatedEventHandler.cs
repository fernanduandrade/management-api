using MediatR;
using Shop.Application.Common.Interfaces;
using Shop.Domain.Products;
using Shop.Domain.Products.Errors;
using Shop.Domain.SalesHistory.Events;

namespace Shop.Application.SalesHistory.Events;
public sealed class SaleCreatedEventHandler : INotificationHandler<SaleCreatedEvent>
{
  private readonly IProductRepository _productRepository;
  private readonly IUnitOfWork _unitOfWork;

  public SaleCreatedEventHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
   => (_productRepository, _unitOfWork) = (productRepository, unitOfWork);
    public async Task Handle(SaleCreatedEvent notification, CancellationToken cancellationToken)
    {
        var product = await _productRepository.FindByIdAsync(notification.ProductId);

        if(product is null)
            throw new InvalidProductException("Produto não encontrado."); 
        
        product.SetQuantity(notification.Quantity);
        _productRepository.Update(product);
        await _unitOfWork.Commit(cancellationToken);

    }
}
