using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Clients.AddInput;
using Shop.Application.Clients.CreateClient;
using Shop.Application.Clients.DeleteBulk;
using Shop.Application.Clients.DeleteClient;
using Shop.Application.Clients.Dtos;
using Shop.Application.Clients.GetAllClientPaginated;
using Shop.Application.Clients.GetClientById;
using Shop.Application.Clients.UpdateClient;
using Shop.Application.Clients.UpdateStatus;
using Shop.Application.Common.Models;
using Shop.Presentation.Controllers.Base;

namespace Shop.Presentation.Controllers;

public class ClientsController : BaseController
{
    [HttpPost]
    public async Task<ActionResult> Create(CreateClientCommand command)
    {
        var result = await Mediator.Send(command);

        return Created("", result);
    }

    [HttpPut]
    public async Task<ActionResult> Update(UpdateClientCommand command)
    {
        var result = await Mediator.Send(command);

        return Ok(result);
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete([FromRoute] DeleteClientCommand command)
    {
        var result = await Mediator.Send(command);
        if (result.Type == ResponseTypeEnum.Error) return BadRequest(result);
        return Ok(result);
    }
    
    [HttpGet]
    public async Task<ActionResult<ApiResult<PaginatedList<ClientDto>>>> GetAll([FromQuery] GetAllClientPaginatedQuery query)
    {
        var result = await Mediator.Send(query);
    
        return Ok(result);
    }
    
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResult<ClientDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResult<ClientDto>>> GetClientById([FromRoute] GetClientByIdQuery query)
    {
        var result = await Mediator.Send(query);
    
        return Ok(result);
    }


    [HttpDelete("bulk")]
    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status204NoContent)]
    public async Task<ActionResult<ApiResult>> DeleteBulk([FromBody] DeleteClientsBulkCommand command)
    {
        var result = await Mediator.Send(command);
    
        return Ok(result);
    }

    [HttpPost("update-balance")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateBalance([FromBody] AddInputCommand command)
    {
        _ = await Mediator.Send(command);

        return NoContent();
    }

    [HttpPost("update-status")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateStatus([FromBody] UpdateStatusCommand command)
    {
        _ = await Mediator.Send(command);

        return NoContent();
    }
}