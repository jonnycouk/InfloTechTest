namespace UserManagement.Sdk.Model;

public class UserDto
{
    public long Id { get; set; }
    public string? Forename { get; set; }
    public string? Surname { get; set; }
    public string? JobTitle { get; set; }
    public string? Organisation { get; set; }
    public string? Email { get; set; }
    public bool IsActive { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string? PasswordSalt { get; set; }
    public string? PasswordHash { get; set; }    
}