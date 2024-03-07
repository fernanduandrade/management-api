using MediatR;
using Shop.Application.Common.Models;

namespace Shop.Application.Orders.DeleteBulk;

public sealed record DeleteOrdersBulkCommand(List<Guid> Ids) : IRequest<ApiResult<Unit>>;
