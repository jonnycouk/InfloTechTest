namespace UserManagement.Services.Domain.Interfaces;

public interface ISecurityService
{
    string GenerateSalt();
    string SaltAndHashPassword(string salt, string password);
    bool PasswordsMatch(string userPassword, string salt, string passwordHash);
}