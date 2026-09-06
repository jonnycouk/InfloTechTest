using System.Collections.Generic;
using UserManagement.Models;

namespace UserManagement.Services.Domain.Interfaces;

public interface ILogService 
{
    IEnumerable<Log> GetAll(int skip, int take);
    void Create(Log log);
    Log? GetById(long id);
    Log? GetByUserId(long id);
}
