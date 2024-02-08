using MediatR;
using Shop.Application.Clients.Dtos;
using Shop.Application.Common.Models;

namespace Shop.Application.Clients.UpdateClient;

public sealed record UpdateClientCommand(Guid Id,
    string Name,
    string LastName,
    bool IsActive,
    string Phone, decimal Debt, decimal Credit) : IRequest<ApiResult<ClientDto>> {}