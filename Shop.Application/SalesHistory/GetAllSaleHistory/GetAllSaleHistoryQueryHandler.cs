using AutoMapper;
using MediatR;
using Shop.Application.Common.Models;
using Shop.Application.SalesHistory.Dtos;
using Shop.Domain.SalesHistory;

namespace Shop.Application.SalesHistory.GetAllSaleHistory;

public sealed class GetAllSaleHistoryQueryHandler : IRequestHandler<GetAllSaleHistoryQuery, ApiResult<PaginatedList<SaleHistoryDto>>>
{
    private readonly IMapper _mapper;
    private readonly ISaleHistoryRepository _saleHistoryRepository;

    public GetAllSaleHistoryQueryHandler(ISaleHistoryRepository saleHistoryRepository, IMapper mapper)
        => (_saleHistoryRepository, _mapper) = (saleHistoryRepository, mapper);
    
    public async Task<ApiResult<PaginatedList<SaleHistoryDto>>> Handle(GetAllSaleHistoryQuery request, CancellationToken cancellationToken)
    {
        var records = _saleHistoryRepository.GetAllPaginated();
        var pagination = await PaginatedList<SaleHistory>
            .CreateAsync(records, request.PageNumber, request.PageSize);
        var dto = _mapper.Map<List<SaleHistoryDto>>(pagination.Items);
        
        var result = new PaginatedList<SaleHistoryDto>(dto, pagination.TotalCount, request.PageNumber, request.PageSize);
        return new ApiResult<PaginatedList<SaleHistoryDto>>(result, ResponseTypeEnum.Success,"Operation completed successfully.");
    }
}