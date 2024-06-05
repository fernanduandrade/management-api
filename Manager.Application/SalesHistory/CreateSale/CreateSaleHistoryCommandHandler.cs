using AutoMapper;
using MediatR;
using Manager.Application.Common.Interfaces;
using Manager.Application.Common.Models;
using Manager.Application.SalesHistory.Dtos;
using Manager.Domain.SalesHistory;
using Manager.Domain.SalesHistory.Events;
using Microsoft.Extensions.Logging;

namespace Manager.Application.SalesHistory.CreateSale;

public sealed class CreateSaleHistoryCommandHandler(IUnitOfWork unitOfWork,
        ISaleHistoryRepository saleHistoryRepository,
        IMapper mapper,
        ILogger<CreateSaleHistoryCommandHandler> logger)
        : IRequestHandler<CreateSaleHistoryCommand, ApiResult<SaleHistoryDto>>
{
    
    public async Task<ApiResult<SaleHistoryDto>> Handle(CreateSaleHistoryCommand request, CancellationToken cancellationToken)
    {
        var entity = SaleHistory.Create(request.ClientName,
            request.Quantity,
            request.PricePerUnit,
            request.ProductId,
            request.PaymentType,
            request.Date);
        
        entity.Raise(new SaleCreatedEvent(entity.ProductId, entity.Quantity));
        saleHistoryRepository.Add(entity);
        await unitOfWork.Commit(cancellationToken);
        var dto = mapper.Map<SaleHistoryDto>(entity);
        logger.LogInformation("Nova venda adicionada para {ClientName}, produto {ProductName}", dto.ClientName, dto.ProductName);
        return new ApiResult<SaleHistoryDto>(dto, ResponseTypeEnum.Success, "Sucesso.");
    }
}