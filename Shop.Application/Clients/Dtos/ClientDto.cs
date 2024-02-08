using AutoMapper;
using Shop.Application.Common.Mapping;
using Shop.Domain.Clients;

namespace Shop.Application.Clients.Dtos;

public sealed record ClientDto : IMapFrom<Client>
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
        profile.CreateMap<Client, ClientDto>();
    }
}