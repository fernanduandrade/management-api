using MediatR;
using Manager.Application.Common.Models;
using Manager.Domain.Orders;
using Microsoft.Extensions.Logging;

namespace Manager.Application.Orders.GetAnalytics;

public class GetAnalyticsQueryHandler(IOrderRepository repository, ILogger<GetAnalyticsQueryHandler> logger)
    : IRequestHandler<GetAnalyticsQuery, ApiResult<AnalyticsDto>>
{
    public Task<ApiResult<AnalyticsDto>> Handle(GetAnalyticsQuery request, CancellationToken cancellationToken)
    {
        int total = repository.Get().Count();
        int closed = repository.Get(x => x.Status == OrderStatus.FECHADO).Count();
        int open = repository.Get(x => x.Status == OrderStatus.ABERTO).Count();

        var dto = new AnalyticsDto(total, closed, open);
        var result = new ApiResult<AnalyticsDto>(dto, ResponseTypeEnum.Success);
        return Task.FromResult((result));
    }
}