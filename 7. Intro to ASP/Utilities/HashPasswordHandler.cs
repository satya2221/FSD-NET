namespace _7._Intro_to_ASP;

public class HashPasswordHandler
{
    public static string GenerateSalt()
    {
        return BCrypt.Net.BCrypt.GenerateSalt(12);
    }
    public static string GenerateHash(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password, GenerateSalt());
    }
    public static bool VerifyPassword(string password, string hashPassword)
    {
        return BCrypt.Net.BCrypt.Verify(password, hashPassword);
    }
}
