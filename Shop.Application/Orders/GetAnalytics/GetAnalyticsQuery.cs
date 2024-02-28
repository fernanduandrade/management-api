using MediatR;
using Shop.Application.Common.Models;

namespace Shop.Application.Orders.GetAnalytics;

public sealed record GetAnalyticsQuery() : IRequest<ApiResult<AnalyticsDto>>;