using UserManagement.Models;
using UserManagement.Web.Models.Logs;

namespace UserManagement.Web.ViewModels;

public class UserViewModel
{
    public int MaxLogCount { get; set; }
    public User User { get; set; } = new();
    public List<LogListItemViewModel>? Logs { get; set; } = new();
}