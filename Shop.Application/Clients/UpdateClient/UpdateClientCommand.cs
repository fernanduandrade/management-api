using AutoMapper;
using MediatR;
using Shop.Application.Clients.Dtos;
using Shop.Application.Common.Mapping;
using Shop.Application.Common.Models;
using Shop.Domain.Clients;

namespace Shop.Application.Clients.UpdateClient;

public sealed record UpdateClientCommand : IRequest<ApiResult<ClientDto>>, IMapFrom<Client>
{
    public Guid Id {get; init;}
    public string? Name {get; init;}
    public string? LastName {get; init;}
    public bool IsActive {get; init;}
    public string? Phone {get; init;}
    public decimal Debt {get; init;}
    public decimal Credit {get; init;}

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Client, UpdateClientCommand>();
    }
}