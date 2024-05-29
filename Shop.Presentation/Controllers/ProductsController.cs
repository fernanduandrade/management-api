using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Common.Models;
using Shop.Application.Products.CreateProduct;
using Shop.Application.Products.DeleteBulk;
using Shop.Application.Products.DeleteProduct;
using Shop.Application.Products.Dtos;
using Shop.Application.Products.GetAllProductPaginated;
using Shop.Application.Products.GetAutoComplete;
using Shop.Application.Products.GetProductById;
using Shop.Application.Products.UpdateProduct;
using Shop.Presentation.Controllers.Base;

namespace Shop.Presentation.Controllers;

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