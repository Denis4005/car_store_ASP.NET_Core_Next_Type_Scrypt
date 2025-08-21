using backend.core.Abstraction;
using backend.core.Enums;
using Backend.Core.Abstraction;


namespace Backend.Application.Services;

public class PermissionService : IPermissionService
{
    private readonly IUsersRepository _usersRepository;
    public PermissionService(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }

    public Task<HashSet<Permission>> GetPermissionAsync(Guid userId)
    {
        return _usersRepository.GetUserPermission(userId);
    }
}

