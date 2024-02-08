using AutoMapper;
using MediatR;
using Shop.Application.Common.Models;
using Shop.Application.SalesHistory.Dtos;
using Shop.Domain.SalesHistory;

namespace Shop.Application.SalesHistory.GetSaleHistoryById;

public sealed class GetSaleHistoryByIdQueryHandler : IRequestHandler<GetSaleHistoryByIdQuery, ApiResult<SaleHistoryDto>>
{
private readonly ISaleHistoryRepository _saleHistoryRepository;
private readonly IMapper _mapper;

public GetSaleHistoryByIdQueryHandler(ISaleHistoryRepository saleHistoryRepository, IMapper mapper)
    => (_saleHistoryRepository, _mapper) = (saleHistoryRepository, mapper);
    
public async Task<ApiResult<SaleHistoryDto>> Handle(GetSaleHistoryByIdQuery request, CancellationToken cancellationToken)
{
    var result = await _saleHistoryRepository.FindByIdAsync(request.Id);

    if(result is null)
        return new ApiResult<SaleHistoryDto>(null, ResponseTypeEnum.Warning,"Failed to find the register.");

    var dto = _mapper.Map<SaleHistoryDto>(result);
    return new ApiResult<SaleHistoryDto>(dto, ResponseTypeEnum.Success, "Operation completed successfully.");
}
}