using MediatR;
using Manager.Application.Common.Models;

namespace Manager.Application.Clients.UpdateStatus;

public sealed record UpdateStatusCommand(Guid id) : IRequest<ApiResult<Unit>>
{
}
