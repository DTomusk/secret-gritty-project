using Api.Auth.DTOs;
using Api.Shared.Extensions;
using Api.Shared.RateLimiting;
using Application.Auth.Commands;
using Application.Auth.DTOs;
using Application.Shared.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Api.Auth.Controllers;

[Route("[controller]")]
[ApiController]
[EnableRateLimiting(RateLimitingConfiguration.AuthPolicy)]
public class AuthController : ControllerBase
{
    private readonly ICommandHandler<LoginUserCommand, AuthResponse> _loginHandler;
    private readonly ICommandHandler<RegisterUserCommand, AuthResponse> _registerHandler;

    public AuthController(
        ICommandHandler<LoginUserCommand, AuthResponse> loginHandler,
        ICommandHandler<RegisterUserCommand, AuthResponse> registerHandler)
    {
        _loginHandler = loginHandler;
        _registerHandler = registerHandler;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserRequest request, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var command = new RegisterUserCommand(request.RegistrationCode, request.Password);
        var authResult = await _registerHandler.HandleAsync(command, cancellationToken);

        return authResult.ToActionResult();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginUserRequest request, CancellationToken cancellationToken)
    {
        var command = new LoginUserCommand(request.UserName, request.Password);
        var authResult = await _loginHandler.HandleAsync(command, cancellationToken);

        return authResult.ToActionResult();
    }

    // Note: this needs to be admin only once we have roles and permissions
    [HttpPost("create")]
    public async Task<IActionResult> CreateUser(CreateUserRequest request, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var command = new CreateUserCommand(request.UserName);
        var authResult = await _registerHandler.HandleAsync(command, cancellationToken);
        return authResult.ToActionResult();
    }
}
