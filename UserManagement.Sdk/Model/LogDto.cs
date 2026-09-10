namespace UserManagement.Sdk.Model;

public class LogDto
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public UserDto? User { get; set; } = new();
    public string Summary { get; set; } = "";
    public string? Detail { get; set; }
    public DateTime CreatedUtc { get; set; }    
    public long? AffectedUserId { get; set; }
    public UserDto? AffectedUser { get; set; } = new();
}