using MediatR;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Shared;
using Shop.Domain.SalesHistory;

namespace Shop.Application.Reports.GetSalesByDate;

public sealed record GetSalesReportByDateQuery(DateTime StartDate, DateTime EndDate) : IRequest<Stream> {}

public sealed class GetSalesReportByDateQueryHandler(ISaleHistoryRepository saleHistoryRepository)
  : IRequestHandler<GetSalesReportByDateQuery, Stream>
{
    public async Task<Stream> Handle(GetSalesReportByDateQuery request, CancellationToken cancellationToken)
    {
        var starDate = DateTime.SpecifyKind(request.StartDate, DateTimeKind.Utc);
        var endDate = DateTime.SpecifyKind(request.EndDate.AddDays(1).AddTicks(-1), DateTimeKind.Utc);
        var records = saleHistoryRepository.GetAll(x => x.Date >= starDate && x.Date <= endDate).Include(x => x.Product);
        var sales = records.ToList();

        SalesReportModel reportModel = new("Nando", starDate, endDate, records.ToList());

        var stream = await Helper.CreateReport(reportModel, "Sales", "PeriodicReport.cshtml");
        return stream;
    }
}
