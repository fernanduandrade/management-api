using AutoMapper;
using Shop.Application.Common.Mapping;
using Shop.Domain.Products;

namespace Shop.Application.Products.Dtos;

public sealed record ProductDto : IMapFrom<Product>
{
    public Guid Id {get; init; }
    public string Description {get; init;}
    public string Name {get; init;}
    public decimal Price {get; init;}
    public int Quantity {get; init;}
    public bool IsAvaliable {get; init;}

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Product, ProductDto>();
    }
}