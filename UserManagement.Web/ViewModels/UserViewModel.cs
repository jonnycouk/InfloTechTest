using System.ComponentModel.DataAnnotations;
using UserManagement.Models;
using UserManagement.Sdk.Model;
using UserManagement.Web.Models.Logs;

namespace UserManagement.Web.ViewModels;

public class UserViewModel
{
    public int MaxLogCount { get; set; }
    public UserDto User { get; set; } = new();
    public List<LogListItemViewModel>? Logs { get; set; } = new();
    
    [Required(ErrorMessage = "Password is required.")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please confirm your password")]
    [DataType(DataType.Password)]
    [Compare(nameof(Password), ErrorMessage = "The password and confirmation password do not match.")]
    [Display(Name = "Confirm Password")]
    public string ConfirmPassword { get; set; } = string.Empty;

}