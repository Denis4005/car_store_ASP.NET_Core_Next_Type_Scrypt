using backend.dataaccess.Reposirories;
using Backend.Dataaccess.Configurations;
using Backend.Dataaccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace backend.dataaccess;

public class CarStoreDbContext(DbContextOptions<CarStoreDbContext> options, IOptions<AuthorizationOptions> authOptions) : DbContext(options)
{
    public DbSet<CarEntity> Car { get; set; }
    public DbSet<UserEntity> User { get; set; }
    public DbSet<RoleEntity> Roles { get; set; }
    public DbSet<UserRoleEntity> UserRoleEntity { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CarStoreDbContext).Assembly,type=>type!=typeof(RolePermissionConfiguration));
        modelBuilder.ApplyConfiguration(new RolePermissionConfiguration(authOptions));
    }
}


