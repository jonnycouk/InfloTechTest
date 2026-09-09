using UserManagement.Models;
using UserManagement.Services.Domain.Interfaces;
using UserManagement.Web;

namespace UserManagement.WebMS.Controllers;

public class HomeController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        ViewData["AppIcon"] = "people.gif";
        return RedirectToAction("List", "Users");
    }
}
