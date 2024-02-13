using Microsoft.AspNetCore.Mvc;
using Shop.Application.OrderProducts.CreateOrderProduct;
using Shop.Application.Orders.CreateOrder;
using Shop.Application.Orders.GetOrderById;
using Shop.Application.Orders.GetOrderPaginated;
using Shop.Application.Products.GetAllProductPaginated;
using Shop.Presentation.Controllers.Base;

namespace Shop.Presentation.Controllers;

public class OrdersController : BaseController
{

    [HttpPost]
    public async Task<IActionResult> Add(CreateOrderCommand command)
    {
        var result = await Mediator.Send(command);

        return Ok(result);
    }
    
    [HttpPost("add-product")]
    public async Task<IActionResult> Add(CreateOrderProductCommand command)
    {
        var result = await Mediator.Send(command);

        return Ok(result);
    }
    
    [HttpGet("status")]
    public async Task<IActionResult> GetByStatus([FromQuery]GetOrderStatusPaginatedQuery query)
    {
        var result = await Mediator.Send(query);

        return Ok(result);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> ById([FromRoute] GetOrderByIdQuery query)
    {
        var result = await Mediator.Send(query);

        return Ok(result);
    }
}