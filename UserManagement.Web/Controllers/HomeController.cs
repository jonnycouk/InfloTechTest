namespace UserManagement.WebMS.Controllers;

public class HomeController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        ViewData["AppIcon"] = "people.gif";
        ViewData["AppTitle"] = "Welcome";
        return RedirectToAction("List", "Users");
    }
}
