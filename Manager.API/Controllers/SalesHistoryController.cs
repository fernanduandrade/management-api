using Microsoft.AspNetCore.Mvc;
using Manager.Application.Common.Models;
using Manager.Application.SalesHistory.CreateSale;
using Manager.Application.SalesHistory.DeleteBulk;
using Manager.Application.SalesHistory.DeleteSaleHistory;
using Manager.Application.SalesHistory.Dtos;
using Manager.Application.SalesHistory.GetAllSaleHistory;
using Manager.Application.SalesHistory.GetMonthSales;
using Manager.Application.SalesHistory.GetSaleHistoryById;
using Manager.Application.SalesHistory.GetTodaySales;
using Manager.Application.SalesHistory.UpdateSaleHistory;
using Manager.Presentation.Controllers.Base;

namespace Manager.Presentation.Controllers;

public class SalesHistoryController(ILogger<SalesHistoryController> logger) : BaseController
{
    [HttpPost]
    public async Task<ActionResult> Create(CreateSaleHistoryCommand command)
    {
        var result = await Mediator.Send(command);

        return Created("", result);
    }

    [HttpPut]
    public async Task<ActionResult> Update(UpdateSaleHistoryCommand command)
    {
        var result = await Mediator.Send(command);

        return Ok(result);
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete([FromRoute]DeleteSaleHistoryCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }
    
    [HttpGet]
    public async Task<ActionResult<ApiResult<PaginatedList<SaleHistoryDto>>>> GetAll([FromQuery] GetAllSaleHistoryQuery query)
    {
        var result = await Mediator.Send(query);
    
        return Ok(result);
    }
    
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResult<SaleHistoryDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResult<SaleHistoryDto>>> GetSaleById([FromRoute]GetSaleHistoryByIdQuery query)
    {
        var result = await Mediator.Send(query);
    
        return Ok(result);
    }
    
    [HttpGet("today")]
    [ProducesResponseType(typeof(ApiResult<decimal>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResult<SaleHistoryDto>>> GetTodaySales([FromQuery]GetTodaySaleQuery query)
    {
        var result = await Mediator.Send(query);
    
        return Ok(result);
    }

    [HttpGet("month")]
    [ProducesResponseType(typeof(ApiResult<decimal>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResult<SaleHistoryDto>>> GetMonthSales([FromQuery]GetMonthSaleQuery query)
    {
        var result = await Mediator.Send(query);
    
        return Ok(result);
    }

    [HttpDelete("bulk")]
    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status204NoContent)]
    public async Task<ActionResult<ApiResult>> DeleteBulk([FromBody] DeleteSalesHistoryBulkCommand command)
    {
        var result = await Mediator.Send(command);
    
        return Ok(result);
    }
}