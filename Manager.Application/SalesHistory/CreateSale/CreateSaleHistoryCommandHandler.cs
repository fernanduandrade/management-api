using AutoMapper;
using MediatR;
using Manager.Application.Common.Interfaces;
using Manager.Application.Common.Models;
using Manager.Application.SalesHistory.Dtos;
using Manager.Domain.SalesHistory;
using Manager.Domain.SalesHistory.Events;

namespace Manager.Application.SalesHistory.CreateSale;

public sealed class CreateSaleHistoryCommandHandler : IRequestHandler<CreateSaleHistoryCommand, ApiResult<SaleHistoryDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISaleHistoryRepository _saleHistoryRepository;

    public CreateSaleHistoryCommandHandler(IUnitOfWork unitOfWork,
        ISaleHistoryRepository saleHistoryRepository,
        IMapper mapper)
        => (_unitOfWork, _saleHistoryRepository, _mapper) = (unitOfWork, saleHistoryRepository, mapper);
    
    public async Task<ApiResult<SaleHistoryDto>> Handle(CreateSaleHistoryCommand request, CancellationToken cancellationToken)
    {
        var entity = SaleHistory.Create(request.ClientName,
            request.Quantity,
            request.PricePerUnit,
            request.ProductId,
            request.PaymentType,
            request.Date);
        
        entity.Raise(new SaleCreatedEvent(entity.ProductId, entity.Quantity));
        _saleHistoryRepository.Add(entity);
        await _unitOfWork.Commit(cancellationToken);
        var dto = _mapper.Map<SaleHistoryDto>(entity);
        return new ApiResult<SaleHistoryDto>(dto, ResponseTypeEnum.Success, "Operation completed successfully.");
    }
}