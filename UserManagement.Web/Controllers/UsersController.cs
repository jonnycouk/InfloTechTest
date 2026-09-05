using System.Linq;
using UserManagement.Models;
using UserManagement.Services.Domain.Interfaces;
using UserManagement.Web.Models.Users;

namespace UserManagement.WebMS.Controllers;

[Route("users")]
public class UsersController(IUserService userService) : Controller
{
    [HttpGet("View/{id:long}")]
    public IActionResult View(long id)
    {
        var user = userService.GetById(id);

        if (user == null)
        {
            return RedirectToAction("List");
        }
     
        ViewData["Title"] = $"View User [{user.Id}]";
        return View(user);
    }
    
    [HttpGet("Create")]
    public ViewResult Create()
    {
        ViewData["Title"] = "Create User";
        
        var newUser = new User()
        {
            IsActive = true
        };
        
        return View(newUser);
    }

    [HttpPost("Create")]
    [ValidateAntiForgeryToken]
    public IActionResult Create(User newUser)
    {
        if (!ModelState.IsValid)
        {
            // Return view with validation errors
            return View(newUser);
        }
        
        ViewData["Title"] = "Create";
        
        // Enforce this in case it was changed in post
        newUser.IsActive = false;
        
        userService.Create(newUser);
        
        return RedirectToAction("List");
    }
    
    [HttpGet("List")]
    public ViewResult List(bool? isActive)
    {
        IEnumerable<UserListItemViewModel> items;
        
        if (isActive.HasValue)
        {
            items = GetUsersByActiveState(isActive.Value);
        }
        else
        {
            items = userService.GetAll().Select(user => new UserListItemViewModel
            {
                Id = user.Id,
                Forename = user.Forename,
                Surname = user.Surname,
                Email = user.Email,
                IsActive = user.IsActive,
                DateOfBirth = user.DateOfBirth
            });
            
            ViewData["Title"] = "User List";
        }

        var model = new UserListViewModel
        {
            Items = items.ToList()
        };
        
        return View(model);
    }

    private IEnumerable<UserListItemViewModel> GetUsersByActiveState(bool isActive)
    {
        var items =  userService.FilterByActive(isActive).Select(user => new UserListItemViewModel
        {
            Id = user.Id,
            Forename = user.Forename,
            Surname = user.Surname,
            Email = user.Email,
            IsActive = user.IsActive,
            DateOfBirth = user.DateOfBirth
        });

        if (isActive)
            ViewData["Title"] = "Active Users";
        else
            ViewData["Title"] = "Non Active Users";
        
        return items;
    }
}
