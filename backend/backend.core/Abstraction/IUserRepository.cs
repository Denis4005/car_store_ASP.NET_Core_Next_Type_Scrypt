using backend.core.Enums;
using backend.core.Models;

namespace Backend.Core.Abstraction;

public interface IUsersRepository
{
    Task<Guid> Delete(Guid id);
    Task<List<Users>> Get();
    Task<Guid> Update(Guid id, string email, string username);
    Task<HashSet<Permission>> GetUserPermission(Guid userId);
}
