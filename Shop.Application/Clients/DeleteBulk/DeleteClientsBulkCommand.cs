using MediatR;
using Shop.Application.Common.Models;

namespace Shop.Application.Clients.DeleteBulk;

public sealed record DeleteClientsBulkCommand(List<Guid> Ids) : IRequest<ApiResult<Unit>>;
