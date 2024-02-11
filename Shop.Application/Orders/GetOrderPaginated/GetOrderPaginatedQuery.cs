using MediatR;
using Shop.Application.Common.Models;
using Shop.Application.Orders.Dtos;
using Shop.Domain.Orders;

namespace Shop.Application.Orders.GetOrderPaginated;

public sealed record GetOrderStatusPaginatedQuery : IRequest<ApiResult<PaginatedList<OrderDto>>>
{
    public int PageSize { get; init; } = 1;
    public int PageNumber { get; init; } = 1;
    public OrderStatus Status { get; init; } 
}