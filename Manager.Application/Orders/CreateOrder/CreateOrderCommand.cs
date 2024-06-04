using MediatR;
using Manager.Application.Common.Models;
using Manager.Application.Orders.Dtos;

namespace Manager.Application.Orders.CreateOrder;

public sealed record CreateOrderCommand(
    string ClientName) : IRequest<ApiResult<OrderDto>>;