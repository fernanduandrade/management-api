using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shop.Presentation.Tools;

namespace Shop.Presentation.Controllers;

[ValidationModelState]
[ApiController]
[Route("api/v{version:ApiVersion}/[controller]s")]
public abstract class BaseController : ControllerBase
{
    private ISender _mediator = null;

    protected ISender Mediator =>
        _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
}