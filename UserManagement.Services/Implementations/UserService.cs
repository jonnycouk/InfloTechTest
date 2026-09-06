using System.Collections.Generic;
using System.Linq;
using UserManagement.Data;
using UserManagement.Models;
using UserManagement.Services.Domain.Interfaces;

namespace UserManagement.Services.Domain.Implementations;

public class UserService(IDataContext dataAccess) : IUserService
{
    /// <summary>
    /// Return users by active state
    /// </summary>
    /// <param name="isActive"></param>
    /// <returns></returns>
    public IEnumerable<User> FilterByActive(bool isActive)
    {
        return dataAccess.GetAll<User>().Where(user => user.IsActive == isActive).ToList();
    }


    /// <summary>
    /// Returns all users regardless of active state
    /// </summary>
    /// <returns></returns>
    public IEnumerable<User> GetAll() => dataAccess.GetAll<User>();

    public void Create(User user)
    {
        dataAccess.Create(user);
    }

    public User? GetById(long id)
    {
        return dataAccess.GetById<User>(id);
    }

    public User? GetDetachedEntityById(long id)
    {
        return dataAccess.GetByIdNoTracking<User>(id);
    }

    public void Update(User user)
    {
        dataAccess.Update(user);
    }

    public void Delete(User user)
    {
        dataAccess.Delete(user);
    }

}
