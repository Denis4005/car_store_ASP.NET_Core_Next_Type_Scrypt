

using backend.core.Models;

namespace backend.core.Abstraction;

public interface IAuthRepository
{
    Task Register(Users users);
    Task<Users> GetByEmail(string email);
    Task<Users> GetByUserName(string username);
    Task<string> GetUserRoleAsync(Guid userId);
}
