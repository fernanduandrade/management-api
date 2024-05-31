using MediatR;
using Shop.Application.Common.Interfaces;
using Shop.Domain.SalesHistory;
using Shop.Domain.SalesHistory.Events;

namespace Shop.Application.SalesHistory.Events;

public sealed class CreateBulkSaleEventHandler : INotificationHandler<CreateBulkSaleEvent>
{
    private readonly ISaleHistoryRepository _saleHistoryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateBulkSaleEventHandler(ISaleHistoryRepository saleHistoryRepository, IUnitOfWork unitOfWork)
    {
        _saleHistoryRepository = saleHistoryRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task Handle(CreateBulkSaleEvent notification, CancellationToken cancellationToken)
    {
        List<SaleHistory> saleHistories = new();

        foreach (var product in notification.products)
        {
            var date = DateTime.UtcNow;
            var sale = SaleHistory.Create(notification.clientName,
                product.Quantity,
                product.Product.Price,
                product.ProductId,
                notification.paymentType,
                date);
            
            saleHistories.Add(sale);
        }
        
        _saleHistoryRepository.AddMany(saleHistories);
        await _unitOfWork.Commit(cancellationToken);
    }
}