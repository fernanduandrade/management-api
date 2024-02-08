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
        var result = await _saleHistoryRepository.GetAllPaginated(request.PageSize, request.PageNumber);
        var dtos = _mapper.Map<List<SaleHistoryDto>>(result);
        var paginate = new PaginatedList<SaleHistoryDto>(dtos, dtos.Count, request.PageNumber, request.PageSize);

        return new ApiResult<PaginatedList<SaleHistoryDto>>(paginate, ResponseTypeEnum.Success,"Operation completed successfully.");
    }
}