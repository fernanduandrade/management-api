using AutoMapper;
using Shop.Application.Common.Mapping;
using Entities = Shop.Domain.Entities;

namespace Shop.Application.Sale.DTOs;

public sealed record SaleDTO : IMapFrom<Entities.Sale>
{
    public long Id { get; init; }
    public DateTime SaleDate { get; set; }
    public string ClientName { get; set; }
    public long ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal PricePerUnit { get; set; }
    public decimal TotalPrice { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Entities.Sale, SaleDTO>();
    }
}