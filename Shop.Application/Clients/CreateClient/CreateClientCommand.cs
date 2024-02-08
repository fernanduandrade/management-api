using MediatR;
using Shop.Application.Clients.Dtos;
using Shop.Application.Common.Models;

namespace Shop.Application.Clients.CreateClient;

public sealed record CreateClientCommand(string Name,
    string LastName,
    bool IsActive,
    string Phone,
    decimal Debt,
    decimal Credit): IRequest<ApiResult<ClientDto>> {}