using MediatR;
using Shop.Application.Common.Models;

namespace Shop.Application.Clients.UpdateStatus;

public sealed record UpdateStatusCommand(Guid id) : IRequest<ApiResult<Unit>>
{
}
