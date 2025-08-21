using backend.core.Enums;
using Backend.Dataaccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Dataaccess.Configurations;

public class PermissionConfiguration : IEntityTypeConfiguration<PermissionEntity>
{
    public void Configure(EntityTypeBuilder<PermissionEntity> builder)
    {
        builder.HasKey(x => x.Id);
        
        var permission = Enum
        .GetValues<Permission>().Select(r => new PermissionEntity
        {
            Id = (int)r,
            Name = r.ToString()
        });
        
        builder.HasData(permission);

    }
}

