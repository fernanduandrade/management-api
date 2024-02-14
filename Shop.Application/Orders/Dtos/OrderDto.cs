using AutoMapper;
using Shop.Application.Common.Mapping;
using Shop.Application.Products.Dtos;
using Shop.Domain.Orders;

namespace Shop.Application.Orders.Dtos;

public sealed record OrderDto : IMapFrom<Order>
{
    public Guid Id { get; init; }
    public string? ClientName { get; init; }
    public OrderStatus Status { get; init; }
    public List<ProductDto> Products { get; init; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Order, OrderDto>()
            .ForMember(
                dest => dest.Products,
                opt =>
                    opt.MapFrom(src => src.OrderProducts.Select(op => new ProductDto
                    {
                        Price = op.Product.Price, 
                        Name = op.Product.Name,
                        Quantity = op.Quantity
                    }).ToList())
            );
    }
}