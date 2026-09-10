using Riok.Mapperly.Abstractions;
using UserManagement.Sdk.Model;
using UserManagement.Sdk.Request.Logs;
using UserManagement.Web.Models.Logs;
using UserManagement.Web.Models.Users;

namespace UserManagement.Web.Mapper;

[Mapper]
public partial class LogMapper
{
    private readonly UserMapper _userMapper = new();
    
    [MapperIgnoreTarget(nameof(LogDto.AffectedUser))]
    [MapperIgnoreTarget(nameof(LogDto.User))]
    public partial LogListItemViewModel Map(AddLogRequest request);

    public partial LogListItemViewModel Map(LogDto entity);

    public partial List<LogListItemViewModel> Map(List<LogDto> entities);

    private UserListItemViewModel? MapUser(UserDto? user)
    {
        return user == null ? null : _userMapper.Map(user);
    }
}