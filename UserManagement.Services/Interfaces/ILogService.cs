using System.Collections.Generic;
using UserManagement.Models;

namespace UserManagement.Services.Domain.Interfaces;

public interface ILogService 
{
    IEnumerable<Log> GetAll(int skip, int take);
    void Create(Log log);
    User? GetById(long id);
    User? GetByUserId(long id);
}
