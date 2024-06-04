using MediatR;
using Manager.Application.Common.Models;

namespace Manager.Application.Products.DeleteProduct;

public sealed record DeleteProductCommand(Guid id) : IRequest<ApiResult<Unit>>  {}