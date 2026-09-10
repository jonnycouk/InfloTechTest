using UserManagement.Sdk.Model;

namespace UserManagement.ApiServices.Users;

public interface IUserApiService 
{
    /// <summary>
    /// Get all users
    /// </summary>
    /// <returns></returns>
    IEnumerable<UserDto> GetAll(string filter);
    
    /// <summary>
    /// Create new user
    /// </summary>
    /// <param name="user"></param>
    void Create(UserDto user);
    
    /// <summary>
    /// Get user by ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    UserDto? GetById(long id);

    /// <summary>
    /// Returns a detached/non-tracked entity
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    UserDto? GetDetachedEntityById(long id);
    
    /// <summary>
    /// Update given user 
    /// </summary>
    /// <param name="user"></param>
    void Update(UserDto user);
    
    /// <summary>
    /// Deleted given user
    /// </summary>
    /// <param name="user"></param>
    void Delete(UserDto user);
}