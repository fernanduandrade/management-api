using AutoMapper;
using MediatR;
using Manager.Application.Clients.Dtos;
using Manager.Application.Common.Mapping;
using Manager.Application.Common.Models;
using Manager.Domain.Clients;

namespace Manager.Application.Clients.UpdateClient;

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