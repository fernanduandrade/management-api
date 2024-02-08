using MediatR;
using Shop.Application.Clients.Dtos;
using Shop.Application.Common.Models;

namespace Shop.Application.Clients.GetClientById;

public sealed record GetClientByIdQuery(Guid Id) : IRequest<ApiResult<ClientDto>>;