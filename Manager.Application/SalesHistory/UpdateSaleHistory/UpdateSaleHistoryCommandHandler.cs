using AutoMapper;
using MediatR;
using Manager.Application.Common.Interfaces;
using Manager.Application.Common.Models;
using Manager.Application.SalesHistory.Dtos;
using Manager.Domain.SalesHistory;

namespace Manager.Application.SalesHistory.UpdateSaleHistory;

public sealed class UpdateSaleHistoryCommandHandler : IRequestHandler<UpdateSaleHistoryCommand, ApiResult<SaleHistoryDto>>
{
    private readonly ISaleHistoryRepository _saleHistoryRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public UpdateSaleHistoryCommandHandler(IUnitOfWork unitOfWork,
                                            ISaleHistoryRepository saleHistoryRepository,
                                            IMapper mapper)
        => (_unitOfWork, _saleHistoryRepository, _mapper) = (unitOfWork, saleHistoryRepository, mapper);
    
    public async Task<ApiResult<SaleHistoryDto>> Handle(UpdateSaleHistoryCommand request, CancellationToken cancellationToken)
    {
        var saleHistory = await _saleHistoryRepository.FindByIdAsync(request.Id);
        saleHistory.Update(request.ClientName, request.Quantity);
        _saleHistoryRepository.Update(saleHistory);
        await _unitOfWork.Commit(cancellationToken);

        var dto = _mapper.Map<SaleHistoryDto>(saleHistory);
        return new ApiResult<SaleHistoryDto>(dto, ResponseTypeEnum.Success, "Operação completa.");
    }
} 