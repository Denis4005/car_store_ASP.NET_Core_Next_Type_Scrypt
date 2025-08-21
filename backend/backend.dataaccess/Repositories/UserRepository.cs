using backend.core.Enums;
using backend.core.Models;
using backend.dataaccess;
using Backend.Core.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace Backend.Dataaccess.Repositories;

public class UsersRepository : IUsersRepository
{
    private readonly CarStoreDbContext _context;
    public UsersRepository(CarStoreDbContext context)
    {
        _context = context;
    }
    public async Task<List<Users>> Get()
    {
        var users = new List<Users>();
        var userEntities = await _context.User.AsNoTracking().ToListAsync();
        foreach (var b in userEntities)
        {
            var (user, error) = Users.Register(b.Id, b.Email, b.UserName, b.HashPassword);
            if (user != null)
            {
                users.Add(user);
            }
        }
        return users;
    }
    public async Task<Guid> Update(Guid id, string email, string username)
    {
        await _context.User.Where(b => b.Id == id)
        .ExecuteUpdateAsync(s => s
        .SetProperty(b => b.Email, b => email)
        .SetProperty(b => b.UserName, b => username)
        .SetProperty(b => b.HashPassword, b => b.HashPassword)
        );
        return id;
    }
    public async Task<Guid> Delete(Guid id)
    {
        await _context.User.Where(b => b.Id == id)
        .ExecuteDeleteAsync();
        return id;
    }
    public async Task<HashSet<Permission>> GetUserPermission(Guid userId)
    {
        var permissions = await _context.User
            .AsNoTracking()
            .Where(u => u.Id == userId)
            .SelectMany(u => u.Roles.SelectMany(r => r.Permissions))
            .Select(p => (Permission)p.Id)
            .ToHashSetAsync();

        return permissions;
    }
}