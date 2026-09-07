using System.Linq;
using UserManagement.Models;
using UserManagement.Services.Domain.Interfaces;
using UserManagement.Web.Mapper;
using UserManagement.Web.Models.Logs;

namespace UserManagement.WebMS.Controllers;

public class LogsController(ILogService logService, LogMapper logMapper) : Controller
{
    [HttpGet]
    public ViewResult List(int skip = 0, int take = 100)
    {
        //TODO: JW: Add paging

        ViewData["AppIcon"] = "eye.gif";
        ViewData["Title"] = "Log Viewer";

        var items = logService.GetAll(skip, take).ToList();

        var model = new LogListViewModel
        {
            Items = logMapper.Map(items.ToList())
        };
        
        return View(model);
    }
    
    [HttpGet]
    public ViewResult View(long id)
    {
        ViewData["AppIcon"] = "eye.gif";
        ViewData["Title"] = "Log Viewer";

        var model = logMapper.Map(logService.GetById(id) ?? new Log());
        return View(model);
    }
}
