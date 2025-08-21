namespace Backend.Dataaccess.Entities;

public class UserEntity
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string HashPassword { get; set; } = string.Empty;
    public ICollection<RoleEntity> Roles { get; set; } = [];
    public ICollection<CarEntity> Car { get; set; } = [];
}

