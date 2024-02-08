using MediatR;
using Shop.Application.Common.Models;

namespace Shop.Application.SalesHistory.DeleteSaleHistory;

public sealed record DeleteSaleHistoryCommand(Guid Id) : IRequest<ApiResult> {}