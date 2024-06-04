namespace Manager.Application.Reports.GetSalesByDate;
using Manager.Domain.SalesHistory;

public sealed record SalesReportModel(
    string IssuedBy,
    DateTime StartDate,
    DateTime EndDate,
    List<SaleHistory> Sales
);
