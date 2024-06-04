using AutoMapper;
using MediatR;
using Manager.Application.Common.Models;
using Manager.Application.SalesHistory.Dtos;
using Manager.Domain.SalesHistory;

namespace Manager.Application.SalesHistory.GetAllSaleHistory;

public sealed class GetAllSaleHistoryQueryHandler : IRequestHandler<GetAllSaleHistoryQuery, ApiResult<PaginatedList<SaleHistoryDto>>>
{
    private readonly IMapper _mapper;
    private readonly ISaleHistoryRepository _saleHistoryRepository;

    public GetAllSaleHistoryQueryHandler(ISaleHistoryRepository saleHistoryRepository, IMapper mapper)
        => (_saleHistoryRepository, _mapper) = (saleHistoryRepository, mapper);
    
    public async Task<ApiResult<PaginatedList<SaleHistoryDto>>> Handle(GetAllSaleHistoryQuery request, CancellationToken cancellationToken)
    {
        var records = _saleHistoryRepository.GetAllPaginated();
        var pagination = await PaginatedList<SaleHistoryDto>
            .CreateAsync(records, request.PageNumber, request.PageSize, _mapper);
        return new ApiResult<PaginatedList<SaleHistoryDto>>(pagination, ResponseTypeEnum.Success,"Operation completed successfully.");
    }
}