using backend.core.Abstraction;
using backend.core.Models;

namespace backend.application.Services;

public class AuthService : IAuthService
{
    private readonly IAuthRepository _authRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtProvider _jwtProvider;

    public AuthService(IAuthRepository authRepository, IPasswordHasher passwordHasher, IJwtProvider jwtProvider)
    {
        _authRepository = authRepository;
        _passwordHasher = passwordHasher;
        _jwtProvider = jwtProvider;
    }

    public async Task<string> UserRegister(string email, string username, string password)
    {
        try
        {
            await _authRepository.GetByEmail(email);

            return "Такая почта зарегистрирована";
        }
        catch
        {
            try
            {
                await _authRepository.GetByUserName(username);

                return "Такое имя зарегистрировано";
            }
            catch
            {
                if (password == "")
                {
                    return "Пароль не может быть пустым";
                }

                var hashpassword = _passwordHasher.Generate(password);

                var (user, error) = Users.Register(Guid.NewGuid(), email, username, hashpassword);

                if (user != null)
                {
                    await _authRepository.Register(user);
                }

                return error;
            }
        }
    }
    public async Task<(string token, string error)> UserLogin(string email, string password)
    {
        try
        {
            var user = await _authRepository.GetByEmail(email);

            var result = _passwordHasher.Verify(password, user.HashPassword);

            if (result == false)
            {
                return ("","Ошибка Аутентификации");
            }

            var roles = await _authRepository.GetUserRoleAsync(user.Id);

            if (roles != null && roles != "no")
            {
                var token = _jwtProvider.GenerateToken(user, roles);

                return (token,"");
            }
            
            return ("", "Роль не найдена");
        }
        catch
        {
            return ("","Почта не зарегистрирована");
        }
    }
}
