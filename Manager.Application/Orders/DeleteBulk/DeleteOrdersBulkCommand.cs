using MediatR;
using Manager.Application.Common.Models;

namespace Manager.Application.Orders.DeleteBulk;

public sealed record DeleteOrdersBulkCommand(List<Guid> Ids) : IRequest<ApiResult<Unit>>;
