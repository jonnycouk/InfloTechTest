namespace UserManagement.Sdk.Request;

public class UpdateUserRequest
{
    public long Id { get; set; }
    public string? Forename { get; set; }
    public string? Surname { get; set; }
    public string? JobTitle { get; set; }
    public string? Organisation { get; set; }
    public string? Email { get; set; }
    public bool IsActive { get; set; }
    public DateTime DateOfBirth { get; set; }
}