using System.Collections.Generic;
using Riok.Mapperly.Abstractions;
using UserManagement.Models;
using UserManagement.Sdk.Model;
using UserManagement.Sdk.Request.Logs;

namespace UserManagement.Api.Mapper;

[Mapper]
public partial class LogMapper
{
    private readonly UserMapper _userMapper = new();
    
    [MapperIgnoreTarget(nameof(Log.AffectedUser))]
    [MapperIgnoreTarget(nameof(Log.User))]
    public partial Log Map(AddLogRequest request);

    public partial LogDto Map(Log entity);

    public partial List<LogDto> Map(List<Log> entities);

    private UserDto? MapUser(User? user)
    {
        return user == null ? null : _userMapper.Map(user);
    }
}