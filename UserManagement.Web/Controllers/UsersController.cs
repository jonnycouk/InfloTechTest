using System;
using System.Linq;
using System.Text.Json;
using UserManagement.Models;
using UserManagement.Services.Domain.Interfaces;
using UserManagement.Web;
using UserManagement.Web.Models.Logs;
using UserManagement.Web.Models.Users;
using UserManagement.Web.ViewModels;

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
        
        var previousUserDetail = userService.GetDetachedEntityById(user.Id);
        string jsonDetail = JsonSerializer.Serialize(previousUserDetail);
        logService.Create(new Log { Summary = $"{SystemLogEntry.UserAccountDeleted}: ID: {user.Id}",  Detail = $"User: {jsonDetail}" });

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
        
        logService.Create(new Log { Summary = SystemLogEntry.UserAccountOpenedToEdit, AffectedUser = user });
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
        
        var previousUserDetail = userService.GetDetachedEntityById(user.Id);
        userService.Update(user);
        
        string jsonDetail = JsonSerializer.Serialize(previousUserDetail);
        logService.Create(new Log { Summary = $"{SystemLogEntry.UserAccountEdited}: ID: {user.Id}", AffectedUser = user, Detail = $"Previous Value: {jsonDetail}"});

        return RedirectToAction("List");
    }
    
    
    [HttpGet("View/{id:long}")]
    public IActionResult View(long id)
    {
        var logCount = 20;
        
        var vm = new UserViewModel { MaxLogCount = logCount };
        var user = userService.GetById(id); 
        
        if (user == null)
        {
            return RedirectToAction("List");
        }
        
        vm.User = user;
        ViewData["Title"] = $"View User [{user.Id}]";
        
        logService.Create(new Log { Summary = SystemLogEntry.UserAccountViewed, AffectedUser = vm.User });
        
        var items = (logService.GetByAffectedUserId(user.Id) ?? Array.Empty<Log>())
            .Take(logCount)
            .OrderByDescending(l => l.Id)
            .Select(user => new LogListItemViewModel
        {
            Id = user.Id,
            Summary = user.Summary,
            CreateUtc = user.CreatedUtc,
            User = user.User,
            UserId =  user.UserId,
            AffectedUser =  user.AffectedUser,
            AffectedUserId = user.AffectedUserId,
        });

        vm.Logs = items.ToList();
        
        return View(vm);
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
