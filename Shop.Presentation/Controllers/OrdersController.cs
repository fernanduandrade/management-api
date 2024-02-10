using Microsoft.AspNetCore.Mvc;
using Shop.Application.OrderProducts.CreateOrderProduct;
using Shop.Application.Orders.CreateOrder;
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
}