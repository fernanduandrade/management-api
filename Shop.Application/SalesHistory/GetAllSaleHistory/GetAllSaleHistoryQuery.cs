using MediatR;
using Shop.Application.Common.Models;
using Shop.Application.SalesHistory.Dtos;

namespace Shop.Application.SalesHistory.GetAllSaleHistory;

public record GetAllSaleHistoryQuery : IRequest<ApiResult<PaginatedList<SaleHistoryDto>>>
{
    public int PageSize { get; init; } = 1;
    public int PageNumber { get; init; } = 1;
}