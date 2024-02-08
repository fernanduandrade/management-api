using MediatR;
using Shop.Application.Common.Models;
using Shop.Application.Products.Dtos;

namespace Shop.Application.Products.CreateProduct;

public sealed record CreateProductCommand(string Name, 
    string Description,
    int Quantity,
    decimal Price) : IRequest<ApiResult<ProductDto>> {}