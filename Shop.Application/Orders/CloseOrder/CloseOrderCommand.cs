using MediatR;
using Shop.Application.Common.Models;
using Shop.Domain.SalesHistory;

namespace Shop.Application.Orders.CloseOrder;

public sealed record CloseOrderCommand(Guid OrderId, PaymentType PaymentType) :IRequest<ApiResult<Unit>>;