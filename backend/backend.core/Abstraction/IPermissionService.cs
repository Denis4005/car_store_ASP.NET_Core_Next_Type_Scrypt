
using backend.core.Enums;

namespace backend.core.Abstraction;

public interface IPermissionService
{
    Task<HashSet<Permission>> GetPermissionAsync(Guid userId);
}

