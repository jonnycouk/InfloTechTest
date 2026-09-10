using System.Linq;
using UserManagement.ApiServices.Logs;
using UserManagement.Sdk.Model;
using UserManagement.Web.Mapper;
using UserManagement.Web.Models.Logs;

namespace UserManagement.WebMS.Controllers;

public class LogsController(ILogApiService logApiService, LogMapper logMapper) : Controller
{
    [HttpGet]
    public ViewResult List(int skip = 0, int take = 100)
    {
        //TODO: JW: Add paging

        ViewData["AppIcon"] = "eye.gif";
        ViewData["Title"] = "Log Viewer";

        var items = logApiService.GetAll(skip, take).ToList();

        var model = new LogListViewModel
        {
            Items = logMapper.Map(items)
        };
        
        return View(model);
    }
    
    [HttpGet]
    public ViewResult View(long id)
    {
        ViewData["AppIcon"] = "eye.gif";
        ViewData["Title"] = "Log Viewer";

        var logDto = logApiService.GetById(id);
        var model = logMapper.Map(logDto ?? new LogDto());
        return View(model);
    }
}
