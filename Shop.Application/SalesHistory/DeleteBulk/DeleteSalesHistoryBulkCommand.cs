using MediatR;
using Shop.Application.Common.Models;

namespace Shop.Application.SalesHistory.DeleteBulk;

public sealed record DeleteSalesHistoryBulkCommand(List<Guid> Ids) : IRequest<ApiResult<Unit>>;
