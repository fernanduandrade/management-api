using MediatR;
using Shop.Application.Common.Models;
using Shop.Application.Products.Dtos;

namespace Shop.Application.Products.UpdateProduct;

public class UpdateProductCommand : IRequest<ApiResult<ProductDto>>
{
    public Guid Id {get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public int Quantity { get; init; }
    public decimal Price { get; init; }
}