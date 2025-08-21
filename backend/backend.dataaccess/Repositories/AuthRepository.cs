using backend.core.Abstraction;
using backend.core.Enums;
using backend.core.Models;
using backend.dataaccess;
using Backend.Dataaccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend.Dataaccess.Repositories;

public class AuthRepository : IAuthRepository
{
    private readonly CarStoreDbContext _context;
    public AuthRepository(CarStoreDbContext context)
    {
        _context = context;

    }
    public async Task<string> GetUserRoleAsync(Guid userId)
    {
        var result = await _context.UserRoleEntity
            .Where(ur => ur.UserId == userId)
            .Join(_context.Roles,
                ur => ur.RoleId,
                r => r.Id,
                (ur, r) => r.Name)
            .FirstOrDefaultAsync();
        if (result is not null && result.GetType()==typeof(string))
        {
            return result;
        }
        return "no";
    }
    public async Task Register(Users users)
    {
        var roleEntity = await _context.Roles
        .SingleOrDefaultAsync(r => r.Id == (int)Role.User) ?? throw new Exception();
        var userEntity = new UserEntity
        {
            Id = users.Id,
            Email = users.Email,
            UserName = users.UserName,
            HashPassword = users.HashPassword,
            Roles = [roleEntity],
        };
        await _context.User.AddAsync(userEntity);
        await _context.SaveChangesAsync();
    }

    public async Task<Users> GetByEmail(string email)
    {

        if (string.IsNullOrWhiteSpace(email))
        {
            throw new Exception();
        }

        var userEntity = await _context.User
        .AsNoTracking()
        .FirstOrDefaultAsync(u => u.Email == email) ?? throw new Exception();

        var (user, error) = Users.Register(
        userEntity.Id,
        userEntity.Email,
        userEntity.UserName,
        userEntity.HashPassword);

        if (user == null)
        {
            throw new Exception();
        }
        return user;

    }
    public async Task<Users> GetByUserName(string username)
    {

        if (string.IsNullOrWhiteSpace(username))
        {
            throw new Exception();
        }

        var userEntity = await _context.User
        .AsNoTracking()
        .FirstOrDefaultAsync(u => u.UserName == username) ?? throw new Exception();

        var (user, error) = Users.Register(
        userEntity.Id,
        userEntity.Email,
        userEntity.UserName,
        userEntity.HashPassword);

        if (user == null)
        {
            throw new Exception();
        }
        return user;
        
    }
}