namespace Task.Service.Helpers;

public class PasswordHash
{
    public static (string salt, string passwordHash) Hasher(string password)
    {
        string salt = Guid.NewGuid().ToString();
        string passwordHash = BCrypt.Net.BCrypt.HashPassword(salt + password);
        return (salt, passwordHash);
    }

    public static bool Verify(string password, string passwordHash, string salt)
    {
        return BCrypt.Net.BCrypt.Verify(password, passwordHash + salt);
    }
}

