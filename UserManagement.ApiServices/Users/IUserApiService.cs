using UserManagement.Sdk.Model;
using UserManagement.Sdk.Request.Users;

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
    void Create(CreateUserRequest request);
    
    /// <summary>
    /// Get user by ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    UserDto? GetById(long id);
    
    /// <summary>
    /// Update given user 
    /// </summary>
    /// <param name="user"></param>
    void Update(UpdateUserRequest request);
    
    /// <summary>
    /// Deleted given user
    /// </summary>
    /// <param name="user"></param>
    void Delete(long id);
}