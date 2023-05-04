using Microsoft.AspNetCore.Mvc;
using Shop.Application.Product.Commands;

namespace Shop.Presentation.Controllers;

public class ProductController : BaseController
{
    [HttpPost]
    public async Task<ActionResult> Create(CreateProductCommand command)
    {
        var result = await Mediator.Send(command);

        return Created("", result);
    }
}