namespace Manager.Presentation.Controllers;

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Manager.Application.Reports.GetClientsStatus;
using Manager.Application.Reports.GetProducts;
using Manager.Application.Reports.GetSalesByDate;
using Manager.Presentation.Controllers.Base;

public class ReportsController : BaseController
{

    [HttpGet("sales/periodic")]
    public async Task<IActionResult> GetPeriodicReport([Required ,FromQuery] GetSalesReportByDateQuery query )
    {
        var pdf = await Mediator.Send(query);
        return File(pdf, "application/pdf", "report.pdf");
    }

    [HttpGet("products")]
    public async Task<IActionResult> GetPeriodicReport()
    {
        var query = new GetProductListQuery();
        var pdf = await Mediator.Send(query);
        return File(pdf, "application/pdf", "report.pdf");
    }

    [HttpGet("clients/status")]
    public async Task<IActionResult> GetClientStatus()
    {
        var query = new GetClientsStatusQuery();
        var pdf = await Mediator.Send(query);
        return File(pdf, "application/pdf", "report.pdf");
    }
}