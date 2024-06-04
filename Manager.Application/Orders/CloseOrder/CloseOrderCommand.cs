using MediatR;
using Manager.Application.Common.Models;
using Manager.Domain.SalesHistory;

namespace Manager.Application.Orders.CloseOrder;

public sealed record CloseOrderCommand(Guid OrderId, PaymentType PaymentType) :IRequest<ApiResult<Unit>>;