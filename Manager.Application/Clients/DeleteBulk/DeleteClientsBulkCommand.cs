using MediatR;
using Manager.Application.Common.Models;

namespace Manager.Application.Clients.DeleteBulk;

public sealed record DeleteClientsBulkCommand(List<Guid> Ids) : IRequest<ApiResult<Unit>>;
