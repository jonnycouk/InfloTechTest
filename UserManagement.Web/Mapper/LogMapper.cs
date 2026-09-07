using Riok.Mapperly.Abstractions;
using UserManagement.Models;
using UserManagement.Web.Models.Logs;

namespace UserManagement.Web.Mapper;

[Mapper]
public partial class LogMapper
{
    public partial LogListItemViewModel Map(Log entity);
    public partial List<LogListItemViewModel> Map(List<Log> entities);
}