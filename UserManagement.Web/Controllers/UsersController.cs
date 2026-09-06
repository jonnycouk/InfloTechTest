using System.Linq;
using UserManagement.Models;
using UserManagement.Services.Domain.Interfaces;
using UserManagement.Web;
using UserManagement.Web.Models.Users;

namespace UserManagement.WebMS.Controllers;

[Route("users")]
public class UsersController(IUserService userService, ILogService logService) : Controller
{
    [HttpGet("Delete/{id:long}")]
    public IActionResult Delete(long id)
    {
        var user = userService.GetById(id);

        if (user == null)
        {
            return RedirectToAction("List");
        }
        
        ViewData["Title"] = $"Delete User [{user.Id}]";
        
        logService.Create(new Log { Summary = SystemLogEntry.DeleteUserAccountOpened,  AffectedUser = user});
        return View(user);
    }
    
    [HttpPost("ConfirmDeletion")]
    [ValidateAntiForgeryToken]
    public IActionResult ConfirmDeletion(User user)
    {
        ViewData["Title"] = "Delete";
        
        logService.Create(new Log { Summary = $"{SystemLogEntry.UserAccountDeleted}: ID: {user.Id}",  Detail = $"USER: {Json(user)}" });

        userService.Delete(user);
        return RedirectToAction("List");
    }
    
    [HttpGet("Edit/{id:long}")]
    public IActionResult Edit(long id)
    {
        var user = userService.GetById(id);

        if (user == null)
        {
            return RedirectToAction("List");
        }
     
        ViewData["Title"] = $"Edit User [{user.Id}]";
        
        logService.Create(new Log { Summary = SystemLogEntry.UserAccountOpenedToEdit,  Detail = $"{user.Forename} {user.Surname} (ID: {user.Id})", AffectedUser = user });
        return View(user);
    }
    
    [HttpPost("Update")]
    [ValidateAntiForgeryToken]
    public IActionResult Update(User user)
    {
        if (!ModelState.IsValid)
        {
            // Return view with validation errors
            return View("Edit", user);
        }
        
        ViewData["Title"] = "Edit";
        
        var oldUser = userService.GetDetachedEntityById(user.Id);
        userService.Update(user);
        
        logService.Create(new Log { Summary = $"{SystemLogEntry.UserAccountEdited}: ID: {user.Id}", AffectedUser = user, Detail = $"OLD VALUE: {Json(oldUser)}"});
        logService.Create(new Log { Summary = $"{SystemLogEntry.UserAccountEdited}: ID: {user.Id}", AffectedUser = user, Detail = $"NEW VALUE: {Json(user)}"});
        
        return RedirectToAction("List");
    }
    
    
    [HttpGet("View/{id:long}")]
    public IActionResult View(long id)
    {
        var user = userService.GetById(id);

        if (user == null)
        {
            return RedirectToAction("List");
        }
     
        ViewData["Title"] = $"View User [{user.Id}]";
        
        logService.Create(new Log { Summary = SystemLogEntry.UserAccountViewed, AffectedUser = user });
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
    public IActionResult Create(User user)
    {
        if (!ModelState.IsValid)
        {
            // Return view with validation errors
            return View(user);
        }
        
        ViewData["Title"] = "Create";
        
        // Enforce this in case it was changed in post
        user.IsActive = false;
        
        userService.Create(user);
        
        logService.Create(new Log { Summary = SystemLogEntry.UserAccountCreated, AffectedUser = user });
        return RedirectToAction("List");
    }
    
    [HttpGet("List")]
    public ViewResult List(bool? isActive)
    {
        ViewData["AppIcon"] = "people.gif";
        logService.Create(new Log { Summary = SystemLogEntry.UserListViewed });
        
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
        {
            ViewData["Title"] = "Active Users";
            logService.Create(new Log { Summary = SystemLogEntry.ActiveUsersFilterApplied });
        }
        else
        { 
            ViewData["Title"] = "Non Active Users";
            logService.Create(new Log { Summary = SystemLogEntry.NonActiveUsersFilterApplied });
        }
        
        return items;
    }
}
