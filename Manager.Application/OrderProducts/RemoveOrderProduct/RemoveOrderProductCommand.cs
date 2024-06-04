using MediatR;
using Manager.Application.Common.Models;

namespace Manager.Application.OrderProducts.RemoveOrderProduct;

public sealed record RemoveOrderProductCommand(Guid OrderId, Guid ProductId) : IRequest<ApiResult<Unit>>;