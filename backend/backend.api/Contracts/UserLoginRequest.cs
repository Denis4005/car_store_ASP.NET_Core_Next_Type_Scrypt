namespace backend.api.Contracts;

public record UserLoginRequest(
    string Email,
    string Password);
