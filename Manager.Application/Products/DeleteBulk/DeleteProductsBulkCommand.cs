using MediatR;
using Manager.Application.Common.Models;

namespace Manager.Application.Products.DeleteBulk;

public sealed record DeleteProductsBulkCommand(List<Guid> Ids) : IRequest<ApiResult<Unit>>
{
}
