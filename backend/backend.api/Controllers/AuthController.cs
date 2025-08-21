
using backend.api.Contracts;
using backend.core.Abstraction;
using backend.core.Enums;
using backend.infrastructure;
using Microsoft.AspNetCore.Mvc;



namespace backend.api.Controllers;

[ApiController]

public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IResult> Register(UserRegisterRequest request)
    {
        var error = await _authService.UserRegister(request.Email, request.UserName, request.Password);

        if (error != "")
        {
            return Results.BadRequest(error);
        }

        return Results.Ok();
    }

    [HttpPost("login")]
    public async Task<IResult> Login(UserLoginRequest request)
    {
        var (token, error) = await _authService.UserLogin(request.Email, request.Password);

        if (error != "")
        {
            return Results.BadRequest(error);
        }

        // Response.Cookies.Append("car-cookies", token, new CookieOptions
        // {
        //     HttpOnly = true,
        //     Expires = DateTimeOffset.UtcNow.AddHours(1),

        //     //Для разработки
        //     Secure = false,
        //     SameSite = SameSiteMode.Lax,

        //     //Для продакшена
        //     // Secure = true,
        //     // SameSite = SameSiteMode.Strict,

        // });

        return Results.Ok("Bearer " + token);
    }
    [HttpGet("health-token")]
    [RequirePermissions(Permission.Read)]
    public Task<IResult> HelthToken()
    {
        return Task.FromResult(Results.Ok());
    }
}
