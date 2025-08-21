using backend.core.Enums;
using backend.dataaccess.Reposirories;
using Backend.Dataaccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Options;

namespace Backend.Dataaccess.Configurations;

public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermissionEntity>
{
    private readonly AuthorizationOptions _authorizationOptions;
    public RolePermissionConfiguration(IOptions<AuthorizationOptions> authorization)
    {
        _authorizationOptions = authorization.Value;
    }
    public void Configure(EntityTypeBuilder<RolePermissionEntity> builder)
    {
        builder.HasKey(r => new { r.RoleId, r.PermissionId });
        builder.HasData(ParseRolePermissions());
    }
    private RolePermissionEntity[] ParseRolePermissions()
    {
        return _authorizationOptions.RolePermissions.SelectMany(r => r.Permission.Select(p => new RolePermissionEntity
        {
            RoleId = (int)Enum.Parse<Role>(r.Role),
            PermissionId = (int)Enum.Parse<Permission>(p)
        })).ToArray();
    }
}

