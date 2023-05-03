using Microsoft.AspNetCore.Mvc;

namespace Shop.Presentation.Controllers;

public class HealthTestController : BaseController
{
    [HttpGet]
    public ActionResult Test()
        => Ok(new { StatusCode = 200, Healthy = "Ok" });
}