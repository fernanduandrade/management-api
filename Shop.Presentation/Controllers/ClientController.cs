using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Client.Commands;
using Shop.Application.Client.DTOs;
using Shop.Application.Client.Queries;
using Shop.Application.Common.Models;

namespace Shop.Presentation.Controllers;

public class ClientController : BaseController
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
    
    [HttpDelete]
    public async Task<ActionResult> Delete(DeleteClientCommand command)
    {
        var result = await Mediator.Send(command);

        return Ok(result);
    }
    
    [HttpGet]
    public async Task<ActionResult<ApiResult<PaginatedList<ClientDTO>>>> GetAll([FromQuery] GetAllClientPaginatedQuery query)
    {
        var result = await Mediator.Send(query);
    
        return Ok(result);
    }
    
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResult<ClientDTO>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResult<ClientDTO>>> GetClientById(long id)
    {
        GetClientByIdQuery query = new() { Id = id};
        var result = await Mediator.Send(query);
    
        return Ok(result);
    }
}