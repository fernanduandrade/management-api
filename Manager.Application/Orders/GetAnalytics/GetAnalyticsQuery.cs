using MediatR;
using Manager.Application.Common.Models;

namespace Manager.Application.Orders.GetAnalytics;

public sealed record GetAnalyticsQuery() : IRequest<ApiResult<AnalyticsDto>>;