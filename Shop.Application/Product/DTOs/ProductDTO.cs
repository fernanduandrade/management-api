using AutoMapper;
using Shop.Application.Common.Mapping;
using Entites = Shop.Domain.Entities;

namespace Shop.Application.Product.DTOs;

public record ProductDTO : IMapFrom<Entites.Product>
{
    public int Id {get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public int Quantity { get; init; }
    public decimal Price { get; init; }
    public bool IsAvaliable { get; init; }

    public ProductDTO(Profile profile)
    {
        profile.CreateMap<Entites.Product, ProductDTO>();
    }
}