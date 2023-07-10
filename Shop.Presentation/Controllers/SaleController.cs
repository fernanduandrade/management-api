using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Common.Models;
using Shop.Application.Sale.Commands;
using Shop.Application.Sale.DTOs;
using Shop.Application.Sale.Queries;

namespace Shop.Presentation.Controllers;

public class SaleController : BaseController
{
    [HttpPost]
    public async Task<ActionResult> Create(CreateSaleCommand command)
    {
        var result = await Mediator.Send(command);

        return Created("", result);
    }

    [HttpPut]
    public async Task<ActionResult> Update(UpdateSaleCommand command)
    {
        var result = await Mediator.Send(command);

        return Ok(result);
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteSaleCommand() { Id = id };
        var result = await Mediator.Send(command);
        if (result.Data)
            return Ok(result);
        return BadRequest(result);
    }
    
    [HttpGet]
    public async Task<ActionResult<ApiResult<PaginatedList<SaleDTO>>>> GetAll([FromQuery] GetAllSalePaginatedQuery query)
    {
        var result = await Mediator.Send(query);
    
        return Ok(result);
    }
    
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResult<SaleDTO>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResult<SaleDTO>>> GetSaleById(long id)
    {
        GetSaleByIdQuery query = new() { Id = id};
        var result = await Mediator.Send(query);
    
        return Ok(result);
    }
}