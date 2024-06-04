using MediatR;
using Manager.Application.Common.Models;
using Manager.Application.Products.Dtos;

namespace Manager.Application.Products.GetProductById;

public sealed record GetProductByIdQuery(Guid Id): IRequest<ApiResult<ProductDto>> {};