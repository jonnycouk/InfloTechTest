using System.Linq;
using UserManagement.Services.Domain.Interfaces;
using UserManagement.Web.Models.Users;

namespace UserManagement.WebMS.Controllers;

[Route("users")]
public class UsersController(IUserService userService) : Controller
{
    [HttpGet]
    public ViewResult List(bool? isActive)
    {
        IEnumerable<UserListItemViewModel> items;
        
        if (isActive.HasValue)
        {
            items = GetUsersByActiveState(isActive.Value);
        }
        else
        {
            items = userService.GetAll().Select(p => new UserListItemViewModel
            {
                Id = p.Id,
                Forename = p.Forename,
                Surname = p.Surname,
                Email = p.Email,
                IsActive = p.IsActive
            });
        }

        var model = new UserListViewModel
        {
            Items = items.ToList()
        };

        ViewData["Title"] = "User List";
        
        return View(model);
    }

    private IEnumerable<UserListItemViewModel> GetUsersByActiveState(bool isActive)
    {
        var items =  userService.FilterByActive(isActive).Select(p => new UserListItemViewModel
        {
            Id = p.Id,
            Forename = p.Forename,
            Surname = p.Surname,
            Email = p.Email,
            IsActive = p.IsActive
        });

        return items;
        
        //ViewData["Title"] = isActive ? "Active Users" : "Inactive Users";
    }
}
