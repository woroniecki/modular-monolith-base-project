using MediatR;
using Microsoft.AspNetCore.Mvc;
using Modules.Core.App.Queries.Test;

namespace Modules.Core.API.Controllers;

[ApiController]
[Route($"api/{ApiName.Name}/[controller]")]
public class TestController(IMediator mediator)
    : ControllerBase
{
    [HttpGet]
    [Route("test")]
    public async Task<ActionResult<string>> GetHabit()
    {
        return Ok((await mediator.Send(new TestQuery())));
    }
}