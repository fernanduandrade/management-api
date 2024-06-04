using MediatR;
using Manager.Application.Common.Models;

namespace Manager.Application.SalesHistory.DeleteSaleHistory;

public sealed record DeleteSaleHistoryCommand(Guid Id) : IRequest<ApiResult<Unit>> {}