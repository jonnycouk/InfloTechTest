using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using UserManagement.Data;
using UserManagement.Models;
using UserManagement.Services.Domain.Interfaces;

namespace UserManagement.Services.Domain.Implementations;

public class LogService(IDataContext dataAccess) : ILogService
{
    public IEnumerable<Log> GetAll(int skip = 0, int take = 50)
    {
        return dataAccess.GetAll<Log>()
            .Include(l => l.User)
            .Include(l => l.AffectedUser)
            .OrderByDescending(l => l.Id)
            .Skip(skip)
            .Take(take)
            .ToList();
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

    public Log? GetById(long id)
    {
        return dataAccess.GetAll<Log>()
            .Include(l => l.User)
            .Include(l => l.AffectedUser)
            .FirstOrDefault(l => l.Id == id);
    }

    public IEnumerable<Log>? GetByAffectedUserId(long id)
    {
        return dataAccess.GetAll<Log>().Where(l => l.AffectedUserId == id).ToList();
    }

    public Log? GetByUserId(long id)
    {
        throw new System.NotImplementedException();
    }
}
