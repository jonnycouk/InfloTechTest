using UserManagement.Models;
using UserManagement.Services.Domain.Interfaces;
using UserManagement.Web;

namespace UserManagement.WebMS.Controllers;

public class HomeController(ILogService logService) : Controller
{
    [HttpGet]
    public ViewResult Index()
    {
        ViewData["AppIcon"] = "people.gif";
        logService.Create(new Log { Summary = SystemLogEntry.ApplicationStarted });
        return View();
    }
}
