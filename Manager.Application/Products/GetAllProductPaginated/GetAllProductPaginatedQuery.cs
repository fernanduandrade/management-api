using MediatR;
using Manager.Application.Common.Models;
using Manager.Application.Products.Dtos;

namespace Manager.Application.Products.GetAllProductPaginated;

public sealed record GetAllProductPaginatedQuery : IRequest<ApiResult<PaginatedList<ProductDto>>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 1;
};