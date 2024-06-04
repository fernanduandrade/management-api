using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Manager.Application.Common.Models;
using Manager.Application.OrderProducts.CreateOrderProduct;
using Manager.Application.OrderProducts.RemoveOrderProduct;
using Manager.Application.Orders.CloseOrder;
using Manager.Application.Orders.CreateOrder;
using Manager.Application.Orders.DeleteBulk;
using Manager.Application.Orders.GetAnalytics;
using Manager.Application.Orders.GetOrderById;
using Manager.Application.Orders.GetOrderPaginated;
using Manager.Application.Products.GetAllProductPaginated;
using Manager.Presentation.Controllers.Base;

namespace Manager.Presentation.Controllers;

public class OrdersController : BaseController
{

    [HttpPost]
    public async Task<IActionResult> Add(CreateOrderCommand command)
    {
        var result = await Mediator.Send(command);

        return Ok(result);
    }
    
    [HttpPost("products/add")]
    public async Task<IActionResult> Add([FromBody]CreateOrderProductCommand command)
    {
        var result = await Mediator.Send(command);

        return Ok(result);
    }
    
    [HttpDelete("products/remove")]
    public async Task<IActionResult> RemoveOrderProduct([FromQuery] RemoveOrderProductCommand command)
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

    [HttpGet("close")]
    public async Task<IActionResult> CloseOrder([FromQuery] CloseOrderCommand command)
    {
        var result = await Mediator.Send(command);

        return Ok(result);
    }
    
    [HttpGet("analytics")]
    public async Task<IActionResult> Analytics([FromQuery] GetAnalyticsQuery query)
    {
        var result = await Mediator.Send(query);

        return Ok(result);
    }

    [HttpDelete("bulk")]
    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status204NoContent)]
    public async Task<ActionResult<ApiResult>> DeleteBulk([FromBody] DeleteOrdersBulkCommand command)
    {
        var result = await Mediator.Send(command);
    
        return Ok(result);
    }
}