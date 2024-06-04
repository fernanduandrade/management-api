using AutoMapper;
using Manager.Application.Common.Mapping;
using Manager.Domain.OrderProducts;

namespace Manager.Application.OrderProducts.Dtos;

public sealed record OrderProductDto : IMapFrom<OrderProduct>
{
    public Guid Id { get; init; }
    public Guid ProductId { get; init; }
    public Guid OrderId { get; init; }
    public int Quantity { get; init; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<OrderProduct, OrderProductDto>();
    }
}