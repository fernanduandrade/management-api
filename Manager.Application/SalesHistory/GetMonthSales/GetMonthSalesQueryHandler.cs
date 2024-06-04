using MediatR;
using Manager.Application.Common.Models;
using Manager.Domain.SalesHistory;

namespace Manager.Application.SalesHistory.GetMonthSales;

public class GetMonthSalesQueryHandler : IRequestHandler<GetMonthSaleQuery, ApiResult<decimal>>
{
  private readonly ISaleHistoryRepository _saleHistoryRepository;

    public GetMonthSalesQueryHandler(ISaleHistoryRepository saleHistoryRepository)
    {
      _saleHistoryRepository = saleHistoryRepository;
    }
    public Task<ApiResult<decimal>> Handle(GetMonthSaleQuery request, CancellationToken cancellationToken)
    {
      DateTime compareDate = DateTime.UtcNow;
      
      var salesOfTheDay = _saleHistoryRepository
        .Get(x => x.Date.Month == compareDate.Month)
        .Sum(x => x.TotalPrice);

        var result = new ApiResult<decimal>(salesOfTheDay, ResponseTypeEnum.Success);
        return Task.FromResult(result);
    }
}
