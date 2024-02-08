using MediatR;
using Shop.Application.Common.Models;
using Shop.Application.Products.Dtos;

namespace Shop.Application.Products.GetProductById;

public sealed record GetProductByIdQuery(Guid Id): IRequest<ApiResult<ProductDto>> {};