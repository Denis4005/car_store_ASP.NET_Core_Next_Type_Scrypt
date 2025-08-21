namespace backend.core.Abstraction;

public interface IPasswordHasher
{
    string Generate(string password);
    bool Verify(string password, string hashPassword);
}
