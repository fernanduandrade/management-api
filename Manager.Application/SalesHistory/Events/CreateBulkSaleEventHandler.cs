using MediatR;
using Manager.Application.Common.Interfaces;
using Manager.Domain.SalesHistory;
using Manager.Domain.SalesHistory.Events;
using Microsoft.Extensions.Logging;

namespace Manager.Application.SalesHistory.Events;

public sealed class CreateBulkSaleEventHandler(ISaleHistoryRepository saleHistoryRepository, IUnitOfWork unitOfWork, ILogger<CreateBulkSaleEventHandler> logger)
    : INotificationHandler<CreateBulkSaleEvent>
{
    private readonly ISaleHistoryRepository _saleHistoryRepository = saleHistoryRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task Handle(CreateBulkSaleEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Iniciado processo para adicionar lote de produtos vendidos");
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