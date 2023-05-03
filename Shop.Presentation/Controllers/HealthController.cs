using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Shop.Presentation.Controllers;

[ApiController]
[Route("api/health")]
[Authorize]
public class HealthController : ControllerBase
{
    [HttpGet]
    public ActionResult Health()
        => Ok(new { Status = 200, Helath = "Ok" });
}