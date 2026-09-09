using Riok.Mapperly.Abstractions;
using UserManagement.Models;
using UserManagement.Sdk.Model;

namespace UserManagement.Api.Mapper;

[Mapper]
public partial class LogMapper
{
    private readonly UserMapper _userMapper = new();

    public LogDto Map(Log entity)
    {
        var dto = MapToDto(entity);

        if (entity.User != null)
            dto.User = _userMapper.Map(entity.User);

        if (entity.AffectedUser != null)
            dto.AffectedUser = _userMapper.Map(entity.AffectedUser);

        return dto;
    }

    public List<LogDto> Map(List<Log> entities)
    {
        return entities.Select(Map).ToList();
    }

    private partial LogDto MapToDto(Log entity);
}