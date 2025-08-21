
using backend.core.Abstraction;

namespace backend.infrastructure;

public class PasswordHasher : IPasswordHasher
{
    public string Generate(string password)
    {
        return BCrypt.Net.BCrypt.EnhancedHashPassword(password);
    }
    public bool Verify(string password,string hashPassword)
    {
        return BCrypt.Net.BCrypt.EnhancedVerify(password,hashPassword);
    }
}
