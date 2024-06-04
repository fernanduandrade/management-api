using MediatR;
using Manager.Application.Common.Models;
using Manager.Application.OrderProducts.Dtos;

namespace Manager.Application.OrderProducts.CreateOrderProduct;

public sealed record CreateOrderProductCommand(Guid ProductId, Guid OrderId) : IRequest<ApiResult<OrderProductDto>> {}