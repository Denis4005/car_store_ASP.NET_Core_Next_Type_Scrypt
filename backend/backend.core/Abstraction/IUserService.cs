using backend.core.Models;

namespace Backend.Core.Abstraction;

public interface IUsersService
{
    Task<(Guid, string error)> CreateUser(Users users);
    Task<(Guid, string error)> DeleteUser(Guid id);
    Task<List<Users>> GetAllUsers();
    Task<(Guid, string error)> UpdateUser(Guid id, string email, string username);

}
