using AutoMapper;
using Shop.Application.Common.Mapping;
using Shop.Domain.OrderProducts;

namespace Shop.Application.OrderProducts.Dtos;

public sealed record OrderProductDto : IMapFrom<OrderProduct>
{
    public Guid Id { get; init; }
    public Guid ProductId { get; init; }
    public Guid OrderId { get; init; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<OrderProduct, OrderProductDto>();
    }
}