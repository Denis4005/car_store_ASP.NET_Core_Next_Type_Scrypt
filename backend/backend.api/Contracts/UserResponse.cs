namespace Backend.Api.Contracts;

public record UserResponse(
    Guid Id,
    string Email,
    string UserName,
    string HashPassword);

