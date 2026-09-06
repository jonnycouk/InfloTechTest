using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UserManagement.Data;
using UserManagement.Models;
using UserManagement.Services.Domain.Interfaces;

namespace UserManagement.Services.Domain.Implementations;

public class LogService(IDataContext dataAccess) : ILogService
{
    public IEnumerable<Log> GetAll(int skip = 0, int take = 50)
    {
        return dataAccess.GetAll<Log>().Skip(skip).Take(take).ToList().OrderByDescending(l => l.Id);
    }

    public void Create(Log log)
    {
        if (log.User == null)
        {
            // If no user is passed, this is a system audit log
            log.User = dataAccess.GetById<User>(1) ?? new(); // For demo purposes
        }

        dataAccess.Create(log);
    }

    public User? GetById(long id)
    {
        throw new System.NotImplementedException();
    }

    public User? GetByUserId(long id)
    {
        throw new System.NotImplementedException();
    }
}
