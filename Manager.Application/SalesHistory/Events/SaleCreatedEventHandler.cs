using MediatR;
using Manager.Application.Common.Interfaces;
using Manager.Domain.Products;
using Manager.Domain.Products.Errors;
using Manager.Domain.SalesHistory.Events;

namespace Manager.Application.SalesHistory.Events;
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
