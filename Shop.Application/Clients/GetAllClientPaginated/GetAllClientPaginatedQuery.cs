using MediatR;
using Shop.Application.Clients.Dtos;
using Shop.Application.Common.Models;

namespace Shop.Application.Clients.GetAllClientPaginated;

public record GetAllClientPaginatedQuery : IRequest<ApiResult<PaginatedList<ClientDto>>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 1;
}