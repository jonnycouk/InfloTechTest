using System.Linq;
using UserManagement.Services.Domain.Interfaces;
using UserManagement.Web.Models.Logs;

namespace UserManagement.WebMS.Controllers;

public class LogsController(ILogService logService) : Controller
{
    [HttpGet]
    public ViewResult List()
    {
        ViewData["AppIcon"] = "eye.gif";
        ViewData["Title"] = "Log Viewer";
        IEnumerable<LogListItemViewModel> items;
        
        items = logService.GetAll(0, 50).Select(user => new LogListItemViewModel
        {
            Id = user.Id,
            Summary = user.Summary,
            CreateUtc = user.CreatedUtc,
            User = user.User,
            UserId =  user.UserId,
            AffectedUser =  user.AffectedUser,
            AffectedUserId = user.AffectedUserId,
        });

        var model = new LogListViewModel
        {
            Items = items.ToList()
        };
        
        return View(model);
    }
    
    [HttpGet]
    public ViewResult View(long id)
    {
        ViewData["AppIcon"] = "eye.gif";
        ViewData["Title"] = "Log Viewer";
        
        var model = logService.GetById(id);
        return View(model);
    }
}
