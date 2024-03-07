using MediatR;
using Shop.Application.Common.Models;

namespace Shop.Application.Products.DeleteBulk;

public sealed record DeleteProductsBulkCommand(List<Guid> Ids) : IRequest<ApiResult<Unit>>
{
}
