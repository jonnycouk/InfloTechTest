using System;
using System.Linq;
using System.Text.Json;
using UserManagement.ApiServices.Logs;
using UserManagement.ApiServices.Users;
using UserManagement.Sdk.Model;
using UserManagement.Services.Domain.Interfaces;
using UserManagement.Web;
using UserManagement.Web.Mapper;
using UserManagement.Web.Models.Users;
using UserManagement.Web.ViewModels;

namespace UserManagement.WebMS.Controllers;

[Route("users")]
public class UsersController(
    IUserApiService userService, 
    ILogApiService logService, 
    LogMapper logMapper, 
    UserMapper userMapper,
    ISecurityService securityService
    ) : Controller
{
    [HttpGet("Delete/{id:long}")]
    public IActionResult Delete(long id)
    {
        var user = userService.GetById(id);

        if (user == null)
            return RedirectToAction("List");
        
        ViewData["Title"] = $"Delete User [{user.Id}]";
        
        logService.Create(new LogDto { Summary = SystemLogEntry.DeleteUserAccountOpened,  AffectedUser = user});
        return View(user);
    }
    
    [HttpPost("ConfirmDeletion")]
    [ValidateAntiForgeryToken]
    public IActionResult ConfirmDeletion(UserDto user)
    {
        ViewData["Title"] = "Delete";
        
        // Retrieve non-tracked entity before deletion and store it for audit purposes
        var previousUserDetail = userService.GetDetachedEntityById(user.Id);
        string jsonDetail = JsonSerializer.Serialize(previousUserDetail);
        logService.Create(new LogDto { Summary = $"{SystemLogEntry.UserAccountDeleted}: ID: {user.Id}",  Detail = $"User: {jsonDetail}" });

        userService.Delete(user);
        
        TempData["ToastType"] = "success";
        TempData["ToastMessage"] = $"User deleted successfully.";

        return RedirectToAction("List");
    }
    
    [HttpGet("Edit/{id:long}")]
    public IActionResult Edit(long id)
    {
        var user = userService.GetById(id);

        if (user == null)
            return RedirectToAction("List");
     
        ViewData["Title"] = $"Edit User [{user.Id}]";
        
        logService.Create(new LogDto { Summary = SystemLogEntry.UserAccountOpenedToEdit, AffectedUser = user });
        return View(user);
    }
    
    [HttpPost("Update")]
    [ValidateAntiForgeryToken]
    public IActionResult Update(UserDto user)
    {
        if (!ModelState.IsValid)
        {
            return View("Edit", user);
        }

        ViewData["Title"] = "Edit";

        var existingUser = userService.GetById(user.Id);
        
        if (existingUser == null)
        {
            return NotFound();
        }

        // For logging below
        var previousUserDetail = userMapper.Map(existingUser);

        existingUser.Forename = user.Forename;
        existingUser.Surname = user.Surname;
        existingUser.Email = user.Email;
        existingUser.IsActive = user.IsActive;
        existingUser.DateOfBirth = user.DateOfBirth;
        existingUser.Organisation = user.Organisation;
        existingUser.JobTitle = user.JobTitle;

        userService.Update(existingUser);

        TempData["ToastType"] = "success";
        TempData["ToastMessage"] = "User updated successfully.";

        string jsonDetail = JsonSerializer.Serialize(previousUserDetail);
        
        logService.Create(new LogDto 
        { 
            Summary = $"{SystemLogEntry.UserAccountEdited}: ID: {user.Id}", 
            AffectedUser = existingUser, 
            Detail = $"Previous Value: {jsonDetail}"
        });

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
        
        logService.Create(new LogDto { Summary = SystemLogEntry.UserAccountViewed, AffectedUser = vm.User });
        
        var logEntries = (logService.GetByAffectedUserId(user.Id) ?? Array.Empty<LogDto>())
            .OrderByDescending(l => l.Id)
            .Take(logCount)
            .ToList();

        vm.Logs = logMapper.Map(logEntries ?? new List<LogDto>());
        
        return View(vm);
    }
    
    [HttpGet("Create")]
    public ViewResult Create()
    {
        ViewData["Title"] = "Create User";
        var newUser = new UserDto  { IsActive = false };
        return View(new UserViewModel { User  = newUser });
    }

    [HttpPost("Create")]
    [ValidateAntiForgeryToken]
    public IActionResult Create(UserViewModel vm)
    {
        if (!ModelState.IsValid)
        {
            // Return view with validation errors
            return View(vm);
        }
        
        ViewData["Title"] = "Create";
        
        // Enforce this in case it was changed in post
        vm.User.IsActive = false;
        vm.User.PasswordSalt = securityService.GenerateSalt();
        vm.User.PasswordHash = securityService.SaltAndHashPassword(vm.User.PasswordSalt, vm.Password);
        
        userService.Create(vm.User);
        
        TempData["ToastType"] = "success";
        TempData["ToastMessage"] = $"User created successfully.";
        
        logService.Create(new LogDto { Summary = SystemLogEntry.UserAccountCreated, AffectedUser = vm.User });
        return RedirectToAction("List");
    }
    
    [HttpGet("List")]
    public ViewResult List(bool? isActive)
    {
        ViewData["AppIcon"] = "people.gif";
        
        logService.Create(new LogDto { Summary = SystemLogEntry.UserListViewed });
        
        IEnumerable<UserListItemViewModel> items;
        
        string filter = "all";

        if (isActive.HasValue)
        {
            if(isActive == true)
                filter = "active";
            else if(isActive == false)
                filter = "inactive";
        }
            
        var users = userService.GetAll(filter).ToList();
        items = userMapper.Map(users);
            
        var model = new UserListViewModel
        {
            Items = items.ToList()
        };
        
        return View(model);
    }
}
