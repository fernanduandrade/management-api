using AutoMapper;
using MediatR;
using Shop.Application.Common.Interfaces;
using Shop.Application.Common.Models;
using Shop.Application.SalesHistory.Dtos;
using Shop.Domain.SalesHistory;

namespace Shop.Application.SalesHistory.UpdateSaleHistory;

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
        var saleHistory = _mapper.Map<SaleHistory>(request);
        
        _saleHistoryRepository.SetEntityStateModified(saleHistory);
        _saleHistoryRepository.Update(saleHistory);
        await _unitOfWork.Commit(cancellationToken);

        var dto = _mapper.Map<SaleHistoryDto>(saleHistory);
        return new ApiResult<SaleHistoryDto>(dto, ResponseTypeEnum.Success, "Operation completed successfully.");
    }
} 