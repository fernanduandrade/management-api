using AutoMapper;
using Shop.Application.Common.Mapping;
using Shop.Domain.Orders;

namespace Shop.Application.Orders.Dtos;

public sealed record OrderDto : IMapFrom<Order>
{
    public Guid Id { get; init; }
    public string? ClientName { get; init; }
    public OrderStatus Status { get; init; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Order, OrderDto>();
    }
}