using System;
using System.Security.Cryptography;
using System.Text;
using UserManagement.Models;
using UserManagement.Services.Domain.Interfaces;

namespace UserManagement.Services.Domain.Implementations;

public class SecurityService : ISecurityService
{
    public string GenerateSalt()
    {
        const string validChars = "abcd@efghijkl!$mnopqrs(tuvwx)yzABCDEFGH#IJKLMNOPQRS+TUVWXYZ0123456789";
        return RandomNumberGenerator.GetString(validChars, 16);
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