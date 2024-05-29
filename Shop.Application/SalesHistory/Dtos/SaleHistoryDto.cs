using AutoMapper;
using Shop.Application.Common.Mapping;
using Shop.Domain.SalesHistory;

namespace Shop.Application.SalesHistory.Dtos;

public sealed record SaleHistoryDto : IMapFrom<SaleHistory>
{
    public Guid Id { get; init; }
    public DateTime Date { get; init; }
    public string? ClientName { get; init; }
    public Guid ProductId { get; init; }
    public int Quantity { get; init; }
    public decimal PricePerUnit { get; init; }
    public decimal TotalPrice { get; init; }
    public string? ProductName { get; init; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<SaleHistory, SaleHistoryDto>()
            .ForMember(x => x.ProductName, dest => dest.MapFrom(x => x.Product.Name));
    }
}