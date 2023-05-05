using AutoMapper;
using Shop.Application.Common.Mapping;
using Entites = Shop.Domain.Entities;

namespace Shop.Application.Product.DTOs;

public sealed record ProductDTO : IMapFrom<Entites.Product>
{
    public long Id {get; init; }
    public string Description {get; set;}
    public string Name {get; set;}
    public decimal Price {get; set;}
    public int Quantity {get; set;}
    public bool IsAvaliable {get; set;}

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Entites.Product, ProductDTO>();
    }
}