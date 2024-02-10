using MediatR;
using Shop.Application.Common.Models;
using Shop.Application.Orders.Dtos;

namespace Shop.Application.Orders.CreateOrder;

public sealed record CreateOrderCommand(
    string ClientName) : IRequest<ApiResult<OrderDto>>;