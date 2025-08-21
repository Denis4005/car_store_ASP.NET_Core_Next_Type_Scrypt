namespace backend.core.Models;

public class Users
{
    public const int MAX_TITLE_LENGTH = 250;
    private Users(Guid id, string email, string username, string hashpassword)
    {
        Id = id;
        Email = email;
        UserName = username;
        HashPassword = hashpassword;
    }
    public Guid Id { get; }
    public string Email { get; } = string.Empty;
    public string UserName { get; } = string.Empty;
    public string HashPassword { get; } = string.Empty;

    // private static bool IsValidEmail(string email)
    // {
    //     try {
    //         var addr = new System.Net.Mail.MailAddress(email);
    //         return addr.Address == email;
    //     }
    //     catch {
    //         return false;
    //     }
    // }
    private static string ValidateUser(string email, string username, string hashpassword)
    {
        if (string.IsNullOrEmpty(email) || email.Length > MAX_TITLE_LENGTH)
        {
            return "Почта не может быть пустой";
        }
        if (string.IsNullOrEmpty(username) || username.Length > MAX_TITLE_LENGTH)
        {
            return "Имя не может быть пустым";
        }
        if (string.IsNullOrEmpty(hashpassword) || hashpassword.Length > MAX_TITLE_LENGTH)
        {
            return "Пароль не может быть пустым";
        }

        return string.Empty;
    }

    public static (Users? Users, string Error) Register(Guid id, string email, string username, string hashpassword)
    {
        var error = ValidateUser(email, username, hashpassword);
        if (!string.IsNullOrEmpty(error))
        {
            return (null, error);
        }
        var user = new Users(id, email, username, hashpassword);

        return (user, error);
    }

    public static (bool Success, string Error) Update(Guid id, string email, string username)
    {
        if (id == Guid.Empty)
        {
            return (false, "Не существующий ID");
        }
        var error = ValidateUser(email, username, "empty");

        if (!string.IsNullOrEmpty(error))
        {
            return (false, error);
        }
        return (true, error);
    }
    public static (bool Success, string Error) Delete(Guid id)
    {
        if (id == Guid.Empty)
            return (false, "Не существующий ID");

        return (true, string.Empty);
    }
}
