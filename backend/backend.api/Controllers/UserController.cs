using backend.api.Contracts;
using backend.core.Models;
using Backend.Api.Contracts;
using Backend.Core.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Api.Controllers;

[ApiController]
[Route("[controller]")]

public class UserController : ControllerBase
{
    private readonly IUsersService _usersService;
    public UserController(IUsersService usersService)
    {
        _usersService = usersService;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<List<UserResponse>>> GetUsers()
    {
        var users = await _usersService.GetAllUsers();

        var response = users.Select(b => new UserResponse(b.Id, b.Email, b.UserName, b.HashPassword));

        return Ok(response);
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Guid>> CreateUser(UserRegisterRequest request)
    {
        var (user, error) = Users.Register(
            Guid.NewGuid(),
            request.Email,
            request.UserName,
            request.Password
        );
        if (!string.IsNullOrEmpty(error))
        {
            return BadRequest(error);
        }
        if (user is null)
        {
            return BadRequest("Неизвестная ошибка создания автомобиля");
        }

        var (userId, errors) = await _usersService.CreateUser(user);

        if (userId != Guid.Empty)
        {
            return Ok(userId);
        }
        return BadRequest(errors);
    }

    [HttpPut("{id:guid}")]
    [Authorize]
    public async Task<ActionResult<Guid>> UpdatesUser(Guid id, UserRegisterRequest request)
    {
        var (userId,error) = await _usersService.UpdateUser(id, request.Email, request.UserName);

        if (error != "")
        {
            return BadRequest(error);
        }
        return Ok(userId);
    }

    [HttpDelete("{id:guid}")]
    [Authorize]
    public async Task<ActionResult<Guid>> DeleteUser(Guid id)
    {
        var (userId,error) = await _usersService.DeleteUser(id);

        if (error != "")
        {
            return BadRequest(error);
        }

        return Ok(userId);
    }
}
