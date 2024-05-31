namespace Shop.Presentation.Controllers;

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Reports.GetSalesByDate;
using Shop.Presentation.Controllers.Base;

public class ReportsController : BaseController
{

    [HttpGet("sales/periodic")]
    public async Task<IActionResult> GetPeriodicReport([Required ,FromQuery] GetSalesReportByDateQuery query )
    {
        var pdf = await Mediator.Send(query);
        return File(pdf, "application/pdf", "fodase.pdf");
    }
}