using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Clients.CreateClient;
using Shop.Application.Clients.DeleteClient;
using Shop.Application.Clients.Dtos;
using Shop.Application.Clients.GetAllClientPaginated;
using Shop.Application.Clients.GetClientById;
using Shop.Application.Clients.UpdateClient;
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
    
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        DeleteClientCommand command = new(id);
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
    public async Task<ActionResult<ApiResult<ClientDto>>> GetClientById(Guid id)
    {
        GetClientByIdQuery query = new(id);;
        var result = await Mediator.Send(query);
    
        return Ok(result);
    }
}