using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Common.Models;
using Shop.Application.Product.Commands;
using Shop.Application.Product.DTOs;
using Shop.Application.Product.Queries;

namespace Shop.Presentation.Controllers;

public class ProductController : BaseController
{
    [HttpPost]
    public async Task<ActionResult> Create(CreateProductCommand command)
    {
        var result = await Mediator.Send(command);

        return Created("", result);
    }

    [HttpPut]
    public async Task<ActionResult> Update(UpdateProductCommand command)
    {
        var result = await Mediator.Send(command);

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var command = new DeleteProductCommand() { Id = id };
        var result = await Mediator.Send(command);
        if(result.Data)  return Ok(result);

        return BadRequest(result);
    }
    
    [HttpGet]
    public async Task<ActionResult<ApiResult<PaginatedList<ProductDTO>>>> GetAll([FromQuery] GetAllProductPaginatedQuery query)
    {
        var result = await Mediator.Send(query);
    
        return Ok(result);
    }
    
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResult<ProductDTO>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResult<ProductDTO>>> GetProductById(Guid id)
    {
        GetProductByIdQuery query = new() { Id = id};
        var result = await Mediator.Send(query);
    
        return Ok(result);
    }
}