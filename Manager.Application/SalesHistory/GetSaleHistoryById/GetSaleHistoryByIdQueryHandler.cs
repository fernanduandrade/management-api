using AutoMapper;
using MediatR;
using Manager.Application.Common.Models;
using Manager.Application.SalesHistory.Dtos;
using Manager.Domain.SalesHistory;

namespace Manager.Application.SalesHistory.GetSaleHistoryById;

public sealed class GetSaleHistoryByIdQueryHandler(ISaleHistoryRepository saleHistoryRepository, IMapper mapper)
    : IRequestHandler<GetSaleHistoryByIdQuery, ApiResult<SaleHistoryDto>>
{
    public async Task<ApiResult<SaleHistoryDto>> Handle(GetSaleHistoryByIdQuery request, CancellationToken cancellationToken)
{
    var result = await saleHistoryRepository.FindByIdAsync(request.Id);

    if(result is null)
        return new ApiResult<SaleHistoryDto>(null, ResponseTypeEnum.Warning,"Não foi possivél encontrar o registro.");

    var dto = mapper.Map<SaleHistoryDto>(result);
    return new ApiResult<SaleHistoryDto>(dto, ResponseTypeEnum.Success, "Sucesso.");
}
}