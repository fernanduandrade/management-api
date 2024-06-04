using MediatR;
using Manager.Application.Common.Models;
using Manager.Application.Orders.Dtos;
using Manager.Domain.Orders;

namespace Manager.Application.Orders.GetOrderPaginated;

public sealed record GetOrderStatusPaginatedQuery : IRequest<ApiResult<PaginatedList<OrderDto>>>
{
    public int PageSize { get; init; } = 1;
    public int PageNumber { get; init; } = 1;
    public OrderStatus Status { get; init; } 
}