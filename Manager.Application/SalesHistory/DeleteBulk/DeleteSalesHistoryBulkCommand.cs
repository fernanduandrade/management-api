using MediatR;
using Manager.Application.Common.Models;

namespace Manager.Application.SalesHistory.DeleteBulk;

public sealed record DeleteSalesHistoryBulkCommand(List<Guid> Ids) : IRequest<ApiResult<Unit>>;
