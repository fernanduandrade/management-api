using MediatR;
using Manager.Application.Clients.Dtos;
using Manager.Application.Common.Models;

namespace Manager.Application.Clients.GetAllClientPaginated;

public record GetAllClientPaginatedQuery : IRequest<ApiResult<PaginatedList<ClientDto>>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 1;
}