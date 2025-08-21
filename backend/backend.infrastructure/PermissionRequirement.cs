using Microsoft.AspNetCore.Authorization;
using backend.core.Enums;

namespace backend.infrastructure;

public class PermissionRequirement(Permission[] permissions) : IAuthorizationRequirement
{
    public Permission[] Permissions { get; set; } = permissions;
}
