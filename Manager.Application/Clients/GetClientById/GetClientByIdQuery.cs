using MediatR;
using Manager.Application.Clients.Dtos;
using Manager.Application.Common.Models;

namespace Manager.Application.Clients.GetClientById;

public sealed record GetClientByIdQuery(Guid Id) : IRequest<ApiResult<ClientDto>>;