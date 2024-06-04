using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Manager.Application.Common.Models;
using Manager.Application.Products.CreateProduct;
using Manager.Application.Products.DeleteBulk;
using Manager.Application.Products.DeleteProduct;
using Manager.Application.Products.Dtos;
using Manager.Application.Products.GetAllProductPaginated;
using Manager.Application.Products.GetAutoComplete;
using Manager.Application.Products.GetProductById;
using Manager.Application.Products.UpdateProduct;
using Manager.Presentation.Controllers.Base;

namespace Manager.Presentation.Controllers;

public class ProductsController : BaseController
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
    public async Task<ActionResult> Delete([FromRoute] DeleteProductCommand command)
    {
        _ = await Mediator.Send(command);
        
        return NoContent();
    }
    
    [HttpGet]
    public async Task<ActionResult<ApiResult<PaginatedList<ProductDto>>>> GetAll([FromQuery] GetAllProductPaginatedQuery query)
    {
        var result = await Mediator.Send(query);
    
        return Ok(result);
    }
    
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResult<ProductDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResult<ProductDto>>> GetProductById([FromRoute] GetProductByIdQuery  query)
    {
        var result = await Mediator.Send(query);
    
        return Ok(result);
    }
    
    [HttpGet("autocomplete")]
    [ProducesResponseType(typeof(ApiResult<ProductDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResult<ProductDto>>> AutComplete([FromQuery] GetAutoCompleteQuery query)
    {
        var result = await Mediator.Send(query);
    
        return Ok(result);
    }

    [HttpDelete("bulk")]
    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status204NoContent)]
    public async Task<ActionResult<ApiResult>> DeleteBulk([FromBody] DeleteProductsBulkCommand command)
    {
        var result = await Mediator.Send(command);
    
        return Ok(result);
    }
}