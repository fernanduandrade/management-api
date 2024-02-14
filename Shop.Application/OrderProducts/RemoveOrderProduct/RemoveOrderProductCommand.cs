using MediatR;
using Shop.Application.Common.Models;

namespace Shop.Application.OrderProducts.RemoveOrderProduct;

public sealed record RemoveOrderProductCommand(Guid OrderId, Guid ProductId) : IRequest<ApiResult<Unit>>;