using MediatR;
using Shop.Application.Common.Models;
using Shop.Domain.SalesHistory;

namespace Shop.Application.SalesHistory.GetTodaySales;

public class GetTodaySaleQueryHandler : IRequestHandler<GetTodaySaleQuery, ApiResult<decimal>>
{
    private readonly ISaleHistoryRepository _saleHistoryRepository;

    public GetTodaySaleQueryHandler(ISaleHistoryRepository saleHistoryRepository)
    {
        _saleHistoryRepository = saleHistoryRepository;
    }
    public Task<ApiResult<decimal>> Handle(GetTodaySaleQuery request, CancellationToken cancellationToken)
    {

        var salesOfTheDay = _saleHistoryRepository.TodaySales();

        var result = new ApiResult<decimal>(salesOfTheDay, ResponseTypeEnum.Success);
        return Task.FromResult(result);
    }
}