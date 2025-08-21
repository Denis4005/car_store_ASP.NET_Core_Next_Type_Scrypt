using backend.core.Enums;
using Microsoft.AspNetCore.Authorization;


namespace backend.infrastructure;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public class RequirePermissionsAttribute : AuthorizeAttribute
{
    public RequirePermissionsAttribute(params Permission[] permissions)
    {
        string permissionsList = string.Join(",", permissions);

        string policyName = "Permissions:" + permissionsList;

        Policy = policyName;
    }
}
