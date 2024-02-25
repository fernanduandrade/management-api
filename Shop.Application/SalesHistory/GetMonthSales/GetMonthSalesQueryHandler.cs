using MediatR;
using Shop.Application.Common.Models;
using Shop.Domain.SalesHistory;

namespace Shop.Application.SalesHistory.GetMonthSales;

public class GetMonthSalesQueryHandler : IRequestHandler<GetMonthSaleQuery, ApiResult<decimal>>
{
  private readonly ISaleHistoryRepository _saleHistoryRepository;

    public GetMonthSalesQueryHandler(ISaleHistoryRepository saleHistoryRepository)
    {
      _saleHistoryRepository = saleHistoryRepository;
    }
    public Task<ApiResult<decimal>> Handle(GetMonthSaleQuery request, CancellationToken cancellationToken)
    {
      var salesOfTheDay = _saleHistoryRepository.MonthSales();

        var result = new ApiResult<decimal>(salesOfTheDay, ResponseTypeEnum.Success);
        return Task.FromResult(result);
    }
}
