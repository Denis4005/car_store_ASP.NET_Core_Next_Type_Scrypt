using backend.core.Models;

namespace backend.core.Abstraction;

public interface IJwtProvider
{
    string GenerateToken(Users users, string role);
}
