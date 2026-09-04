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
}
