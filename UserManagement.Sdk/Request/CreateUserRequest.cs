namespace UserManagement.Sdk.Request;

public class CreateUserRequest
{
    public string? Forename { get; set; }
    public string? Surname { get; set; }
    public string? JobTitle { get; set; }
    public string? Organisation { get; set; }
    public string? Email { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string PasswordSalt { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
}