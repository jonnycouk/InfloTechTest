using System.Collections.Generic;
using System.Linq;
using UserManagement.Models;
using UserManagement.Sdk.Model;

namespace UserManagement.Api.Mapper;

public class LogMapper
{
    private readonly UserMapper _userMapper = new();

    public LogDto Map(Log entity)
    {
        if (entity == null) return null!;

        return new LogDto
        {
            Id = entity.Id,
            UserId = entity.UserId,
            Summary = entity.Summary,
            Detail = entity.Detail,
            CreatedUtc = entity.CreatedUtc,
            AffectedUserId = entity.AffectedUserId,
            User = entity.User != null ? _userMapper.Map(entity.User) : null!,
            AffectedUser = entity.AffectedUser != null ? _userMapper.Map(entity.AffectedUser) : null
        };
    }

    public List<LogDto> Map(List<Log> entities)
    {
        if (entities == null) return new List<LogDto>();
        return entities.Select(Map).ToList();
    }
}