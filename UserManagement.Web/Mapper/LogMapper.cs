using Riok.Mapperly.Abstractions;
using UserManagement.Models;
using UserManagement.Sdk.Model;
using UserManagement.Web.Models.Logs;

namespace UserManagement.Web.Mapper;

[Mapper]
public partial class LogMapper
{
    public partial LogListItemViewModel Map(LogDto entity);
    public partial List<LogListItemViewModel> Map(List<LogDto> entities);
}