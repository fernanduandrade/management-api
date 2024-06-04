using MediatR;
using Manager.Application.Common.Models;

namespace Manager.Application.Clients.DeleteClient;

public sealed record DeleteClientCommand(Guid Id) : IRequest<ApiResult>;