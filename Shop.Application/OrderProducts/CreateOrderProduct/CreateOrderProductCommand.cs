using MediatR;
using Shop.Application.Common.Models;
using Shop.Application.OrderProducts.Dtos;

namespace Shop.Application.OrderProducts.CreateOrderProduct;

public sealed record CreateOrderProductCommand(Guid ProductId, Guid OrderId) : IRequest<ApiResult<OrderProductDto>> {}