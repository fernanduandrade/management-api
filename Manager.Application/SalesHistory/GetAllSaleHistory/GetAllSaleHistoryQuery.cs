using MediatR;
using Manager.Application.Common.Models;
using Manager.Application.SalesHistory.Dtos;

namespace Manager.Application.SalesHistory.GetAllSaleHistory;

public record GetAllSaleHistoryQuery : IRequest<ApiResult<PaginatedList<SaleHistoryDto>>>
{
    public int PageSize { get; init; } = 1;
    public int PageNumber { get; init; } = 1;
}