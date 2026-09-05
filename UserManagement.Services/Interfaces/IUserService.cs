using System.Collections.Generic;
using UserManagement.Models;

namespace UserManagement.Services.Domain.Interfaces;

public interface IUserService 
{
    /// <summary>
    /// Return users by active state
    /// </summary>
    /// <param name="isActive"></param>
    /// <returns></returns>
    IEnumerable<User> FilterByActive(bool isActive);
    
    /// <summary>
    /// Get all users
    /// </summary>
    /// <returns></returns>
    IEnumerable<User> GetAll();
    
    /// <summary>
    /// Create new user
    /// </summary>
    /// <param name="user"></param>
    void Create(User user);
    
    /// <summary>
    /// Get user by ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    User? GetById(long id);
    
    /// <summary>
    /// Update given user 
    /// </summary>
    /// <param name="user"></param>
    void Update(User user);
}
