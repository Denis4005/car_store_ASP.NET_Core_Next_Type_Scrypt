using backend.core.Models;
using Backend.Dataaccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Dataaccess.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(b => b.Email).HasMaxLength(Users.MAX_TITLE_LENGTH).IsRequired();
        builder.Property(b => b.UserName).HasMaxLength(Users.MAX_TITLE_LENGTH).IsRequired();
        builder.Property(b => b.HashPassword).HasMaxLength(Users.MAX_TITLE_LENGTH).IsRequired();

        builder.HasIndex(u => u.Email)
            .IsUnique();
            
        builder.HasIndex(u => u.UserName)
            .IsUnique();

        builder.HasMany(u => u.Roles).WithMany(r => r.Users).UsingEntity<UserRoleEntity>(
            l => l.HasOne<RoleEntity>().WithMany().HasForeignKey(r => r.RoleId),
            r => r.HasOne<UserEntity>().WithMany().HasForeignKey(u => u.UserId)
        );

    }
}

