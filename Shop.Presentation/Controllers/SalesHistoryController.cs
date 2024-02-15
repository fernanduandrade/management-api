using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Clients.GetClientById;
using Shop.Application.Common.Models;
using Shop.Application.SalesHistory.CreateSale;
using Shop.Application.SalesHistory.DeleteSaleHistory;
using Shop.Application.SalesHistory.Dtos;
using Shop.Application.SalesHistory.GetAllSaleHistory;
using Shop.Application.SalesHistory.GetTodaySales;
using Shop.Application.SalesHistory.UpdateSaleHistory;
using Shop.Presentation.Controllers.Base;

namespace Shop.Presentation.Controllers;

public class SalesHistoryController : BaseController
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
        var result = await Mediator.Send(command);
        if (result.Data)
            return Ok(result);
        return BadRequest(result);
    }
    
    [HttpGet]
    public async Task<ActionResult<ApiResult<PaginatedList<SaleHistoryDto>>>> GetAll([FromQuery] GetAllSaleHistoryQuery query)
    {
        var result = await Mediator.Send(query);
    
        return Ok(result);
    }
    
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResult<SaleHistoryDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResult<SaleHistoryDto>>> GetSaleById([FromRoute]GetClientByIdQuery query)
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
}