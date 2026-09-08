using System;

namespace UserManagement.Web.Models.Users;

public class UserListViewModel
{
    public List<UserListItemViewModel> Items { get; set; } = new();
}

public class UserListItemViewModel
{
    public long Id { get; set; }
    public string? Forename { get; set; }
    public string? Surname { get; set; }
    public string? JobTitle { get; set; }
    public string? Organisation { get; set; }
    public string? Email { get; set; }
    public bool IsActive { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string PasswordSalt { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
}
