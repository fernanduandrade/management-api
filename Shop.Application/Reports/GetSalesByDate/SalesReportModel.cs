namespace Shop.Application.Reports.GetSalesByDate;
using Shop.Domain.SalesHistory;

public sealed record SalesReportModel(
    string IssuedBy,
    DateTime StartDate,
    DateTime EndDate,
    List<SaleHistory> Sales
);
