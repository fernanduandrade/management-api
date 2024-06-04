using MediatR;
using Manager.Application.Common.Models;
using Manager.Domain.SalesHistory;

namespace Manager.Application.SalesHistory.GetTodaySales;

public class GetTodaySaleQueryHandler(ISaleHistoryRepository saleHistoryRepository)
    : IRequestHandler<GetTodaySaleQuery, ApiResult<decimal>>
{
    public Task<ApiResult<decimal>> Handle(GetTodaySaleQuery request, CancellationToken cancellationToken)
    {
        DateTime compareDate = DateTime.UtcNow;

        var salesOfTheDay = saleHistoryRepository
            .Get(x => x.Date.Date == compareDate.Date)
            .Sum(x => x.TotalPrice);

        var result = new ApiResult<decimal>(salesOfTheDay, ResponseTypeEnum.Success);
        return Task.FromResult(result);
    }
}