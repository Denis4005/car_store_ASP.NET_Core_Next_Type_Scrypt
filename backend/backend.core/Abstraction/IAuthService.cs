

namespace backend.core.Abstraction;

public interface IAuthService
{
    Task<(string token, string error)> UserLogin(string email, string password);
    Task<string> UserRegister(string email, string username, string password);
}
