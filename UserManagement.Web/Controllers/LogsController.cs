using System.Linq;
using UserManagement.Services.Domain.Interfaces;
using UserManagement.Web.Models.Logs;

namespace UserManagement.WebMS.Controllers;

public class LogsController(ILogService logService) : Controller
{
    [HttpGet]
    public ViewResult List(int skip = 0, int take = 100)
    {
        //TODO: JW: Add paging
        
        ViewData["AppIcon"] = "eye.gif";
        ViewData["Title"] = "Log Viewer";
        IEnumerable<LogListItemViewModel> items;
        
        items = logService.GetAll(skip, take).Select(user => new LogListItemViewModel
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
