using MediatR;
using Shop.Application.Common.Models;

namespace Shop.Application.Clients.DeleteClient;

public sealed record DeleteClientCommand(Guid Id) : IRequest<ApiResult>;