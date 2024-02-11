using MediatR;
using Shop.Application.Common.Models;
using Shop.Application.Orders.Dtos;

namespace Shop.Application.Orders.GetOrderById;

public sealed record GetOrderByIdQuery(Guid Id) : IRequest<ApiResult<OrderDto>>;