using MediatR;
using Manager.Application.Common.Models;
using Manager.Application.Products.Dtos;

namespace Manager.Application.Products.CreateProduct;

public sealed record CreateProductCommand(string Name, 
    string Description,
    int Quantity,
    decimal Price) : IRequest<ApiResult<ProductDto>> {}