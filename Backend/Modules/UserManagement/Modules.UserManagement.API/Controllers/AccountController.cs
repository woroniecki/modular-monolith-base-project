using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Modules.UserManagement.App.Commands.Login;
using Modules.UserManagement.App.Commands.Register;

namespace Modules.UserManagement.API.Controllers;

[Route($"api/{ApiName.Name}/[controller]")]
[ApiController]
public class AccountController(IMediator mediator, ILogger<AccountController> logger)
    : ControllerBase
{
    [HttpPost]
    [Route("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] RegisterCommand cmd)
    {
        return Ok(await mediator.Send(cmd));
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginCommand cmd)
    {
        logger.LogInformation("Login attempt for user: {Username}", cmd.Username);
        return Ok(await mediator.Send(cmd));
    }
}
