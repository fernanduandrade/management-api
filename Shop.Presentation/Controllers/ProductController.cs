using Microsoft.AspNetCore.Mvc;
using Shop.Application.Product.Commands;
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
    
    [HttpDelete]
    public async Task<ActionResult> Delete(DeleteProductCommand command)
    {
        var result = await Mediator.Send(command);

        return Ok(result);
    }
    
    [HttpGet]
    public async Task<ActionResult> GetAll(GetAllProductPaginatedQuery query)
    {
        var result = await Mediator.Send(query);

        return Ok(result);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult> GetById([FromQuery] GetQuizByIdQuery query)
    {
        var result = await Mediator.Send(query);

        return Ok(result);
    }
}