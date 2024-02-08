using MediatR;
using Shop.Application.Common.Models;
using Shop.Application.Products.Dtos;

namespace Shop.Application.Products.GetAllProductPaginated;

public sealed record GetAllProductPaginatedQuery : IRequest<ApiResult<PaginatedList<ProductDto>>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 1;
};