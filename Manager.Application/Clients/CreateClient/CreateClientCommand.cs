using MediatR;
using Manager.Application.Clients.Dtos;
using Manager.Application.Common.Models;

namespace Manager.Application.Clients.CreateClient;

public sealed record CreateClientCommand(string Name,
    string LastName,
    bool IsActive,
    string Phone,
    decimal Debt,
    decimal Credit): IRequest<ApiResult<ClientDto>> {}