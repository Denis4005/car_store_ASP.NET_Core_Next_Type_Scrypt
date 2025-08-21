using backend.core.Abstraction;
using backend.core.Models;
using Backend.Core.Abstraction;

namespace Backend.Application.Services;

public class UsersService : IUsersService
{
    private readonly IUsersRepository _usersRepository;
    private readonly IAuthRepository _authRepository;
    private readonly IPasswordHasher _passwordHasher;
    public UsersService(IUsersRepository usersRepository, IAuthRepository authRepository, IPasswordHasher passwordHasher)
    {
        _usersRepository = usersRepository;
        _authRepository = authRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<List<Users>> GetAllUsers()
    {
        return await _usersRepository.Get();
    }
    public async Task<(Guid,string error)> CreateUser(Users users)
    {
        try
        {
            await _authRepository.GetByEmail(users.Email);

            return (Guid.Empty, "Такая почта зарегистрирована");
        }
        catch
        {
            if (users.HashPassword == "")
            {
                return (Guid.Empty, "Пароль не может быть пустым");
            }

            var hashpassword = _passwordHasher.Generate(users.HashPassword);

            var (user, error) = Users.Register(Guid.NewGuid(), users.Email, users.UserName, hashpassword);

            if (user != null)
            {
                await _authRepository.Register(user);
                return (user.Id, error);
            }
            return (Guid.Empty, string.Empty);

            
        }
    }
    public async Task<(Guid, string error)> UpdateUser(Guid id, string email, string username)
    {
        try
        {
            if (email == "" || username == "")
            {
                return (Guid.Empty, "Проверьте почту или пароль");
            }
            var user = await _authRepository.GetByEmail(email);

            if (user.Id == id)
            {
                var (isValid, error) = Users.Update(id, email, username);

                if (isValid == true)
                {
                    var userId = await _usersRepository.Update(id, email, username);
                    return (userId, error);
                }
                return (Guid.Empty, string.Empty);
            }
            return (Guid.Empty, "Почта уже зарегистрирована");
            
        }
        catch
        {
            var (isValid, error) = Users.Update(id, email, username);

            if (isValid == true)
            {
                var userId = await _usersRepository.Update(id, email, username);
                return (userId, error);
            }
            return (Guid.Empty, string.Empty);
        }
    }
    public async Task<(Guid, string error)> DeleteUser(Guid id)
    {
       
        var (isValid, error) = Users.Delete(id);

        if (isValid == true)
        {
            var userId = await _usersRepository.Delete(id);
            return (userId, error);
        }
        return (Guid.Empty, string.Empty);
        
    }
}