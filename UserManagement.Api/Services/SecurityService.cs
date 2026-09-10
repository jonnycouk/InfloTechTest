using System.Security.Cryptography;
using System.Text;

namespace UserManagement.Api.Services;

public class SecurityService : ISecurityService
{
    public string GenerateSalt(int length = 16)
    {
        const string validChars = "abcd@efghijkl!$mnopqrs(tuvwx)yzABCDEFGH#IJKLMNOPQRS+TUVWXYZ0123456789";
        return RandomNumberGenerator.GetString(validChars, length);
    }

    public string SaltAndHashPassword(string salt, string password)
    {
        string baseString = $"{salt}:{password}";
        byte[] inputBytes = Encoding.UTF8.GetBytes(baseString);
        byte[] hashBytes = SHA512.HashData(inputBytes);
        return Convert.ToHexString(hashBytes);
    }

    public bool PasswordsMatch(string userPassword, string salt, string passwordHash)
    {
        var hashedUserPassword = SaltAndHashPassword(salt, userPassword);
        return hashedUserPassword == passwordHash;
    }
}