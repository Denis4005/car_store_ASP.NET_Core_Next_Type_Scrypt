namespace backend.api.Contracts;

public record UserRegisterRequest(
    string Email,
    string UserName,
    string Password);
