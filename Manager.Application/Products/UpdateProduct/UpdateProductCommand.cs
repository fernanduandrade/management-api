using MediatR;
using Manager.Application.Common.Models;
using Manager.Application.Products.Dtos;

namespace Manager.Application.Products.UpdateProduct;

public class UpdateProductCommand : IRequest<ApiResult<ProductDto>>
{
    public Guid Id {get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public int Quantity { get; init; }
    public decimal Price { get; init; }
}