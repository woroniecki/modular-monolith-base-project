using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Modules.UserManagement.App.Commands.Login;
using Modules.UserManagement.App.Commands.RefreshLogin;
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
        var response = await mediator.Send(cmd);

        if (!Request.Cookies.TryGetValue("refreshToken", out var refreshToken))
            logger.LogInformation(refreshToken);

        // Store the new refresh token in an HTTP-Only Secure Cookie
        Response.Cookies.Append("refreshToken", response.RefreshToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = false,
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow.AddDays(7)
        });

        return Ok(response);
    }

    [HttpPost]
    [Route("refresh-login")]
    public async Task<IActionResult> RefreshLogin()
    {
        if (!Request.Cookies.TryGetValue("refreshToken", out var refreshToken))
            return Unauthorized("Missing token");

        var command = new RefreshLoginCommand(refreshToken);
        var response = await mediator.Send(command);

        // Store the new refresh token in an HTTP-Only Secure Cookie
        Response.Cookies.Append("refreshToken", response.NewRefreshToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow.AddDays(7)
        });

        return Ok(response);
    }
}
