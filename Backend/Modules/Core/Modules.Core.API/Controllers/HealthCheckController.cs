using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modules.Core.App.Queries.HealthCheck;

namespace Modules.Core.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HealthCheckController(IMediator mediator)
    : ControllerBase
{
    [HttpGet]
    [Route("health-check")]
    [AllowAnonymous]
    public async Task<IActionResult> Register()
    {
        return Ok(await mediator.Send(new HealthCheckQuery()));
    }
}
