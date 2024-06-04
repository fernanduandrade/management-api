using MediatR;
using Manager.Application.Common.Models;
using Manager.Application.Orders.Dtos;

namespace Manager.Application.Orders.GetOrderById;

public sealed record GetOrderByIdQuery(Guid Id) : IRequest<ApiResult<OrderDto>>;