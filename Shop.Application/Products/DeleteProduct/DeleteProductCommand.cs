using MediatR;
using Shop.Application.Common.Models;

namespace Shop.Application.Products.DeleteProduct;

public sealed record DeleteProductCommand(Guid Id) : IRequest<ApiResult>  {}