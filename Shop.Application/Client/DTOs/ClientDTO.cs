using AutoMapper;
using Shop.Application.Common.Mapping;
using Entities = Shop.Domain.Entities;
namespace Shop.Application.Client.DTOs;

public sealed record ClientDTO : IMapFrom<Entities.Client>
{
    public long Id { get; init; }
    public string Name { get; init; }
    public string LastName { get; init; }
    public bool IsActive { get; init; }
    public string Phone { get; init; }
    public decimal Debt { get; init; }
    public decimal Credit { get; init; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Entities.Client, ClientDTO>();
    }
}