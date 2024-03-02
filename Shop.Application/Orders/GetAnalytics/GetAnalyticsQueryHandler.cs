using MediatR;
using Shop.Application.Common.Models;
using Shop.Domain.Orders;

namespace Shop.Application.Orders.GetAnalytics;

public class GetAnalyticsQueryHandler : IRequestHandler<GetAnalyticsQuery, ApiResult<AnalyticsDto>>
{
    private readonly IOrderRepository _orderRepository;

    public GetAnalyticsQueryHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }
    public Task<ApiResult<AnalyticsDto>> Handle(GetAnalyticsQuery request, CancellationToken cancellationToken)
    {
        int total = _orderRepository.GetTotalOrders();
        int closed = _orderRepository.GetTotalClosed();
        int open = _orderRepository.GetTotalOpen();

        var dto = new AnalyticsDto(total, closed, open);
        var result = new ApiResult<AnalyticsDto>(dto, ResponseTypeEnum.Success);
        return Task.FromResult((result));
    }
}