using Microsoft.AspNetCore.Mvc;
using UserManagement.Api.Mapper;
using UserManagement.Sdk.Request;
using UserManagement.Sdk.Request.Users;
using UserManagement.Sdk.Response;
using UserManagement.Sdk.Response.Users;
using UserManagement.Services.Domain.Interfaces;

namespace UserManagement.Api.Feature.Users;

[ApiController]
[Route("v1/[controller]")]
public class UserController (IUserService userService, UserMapper userMapper) : ControllerBase
{
    [HttpGet("Get/{id}")]
    public IActionResult Get(long id)
    {
        var getUserResponse = new GetUserResponse { Success = true, Users = new()};
        
        try
        {
            Models.User? user = userService.GetById(id);

            if (user == null)
                return NotFound();
            
            getUserResponse.Users.Add(userMapper.Map(user));
            getUserResponse.Count = getUserResponse.Users.Count;
            getUserResponse.Message = "User retrieved successfully";
            
            return Ok(getUserResponse);
        }
        catch (Exception)
        {
            getUserResponse.Success = false;
            getUserResponse.Message = "There was an error processing your request";
            return BadRequest(getUserResponse);
        }
    }
    
    [HttpGet("Get/{skip}/{take}")]
    public IActionResult Get(int skip, int take)
    {
        // Enforce some limits
        if(skip < 0) skip = 0;
        if(take < 0) take = 0;
        if(take > 100) take = 100;
        
        var getUserResponse = new GetUserResponse { Success = true, Users = new()};
        
        try
        {
            List<Models.User>? users = userService.GetAll().Skip(skip).Take(take).ToList();

            if (users == null)
                return NotFound();
            
            getUserResponse.Users.AddRange(userMapper.Map(users));
            getUserResponse.Count = getUserResponse.Users.Count;
            getUserResponse.Message = "Users retrieved successfully";
            
            return Ok(getUserResponse);
        }
        catch (Exception)
        {
            getUserResponse.Success = false;
            getUserResponse.Message = "There was an error processing your request";
            return BadRequest(getUserResponse);
        }
    }

    [HttpPut("Update")]
    public IActionResult Update(UpdateUserRequest request)
    {
        var updateUserResponse = new UpdateUserResponse { Success = true };

        try
        {
            var user = userMapper.Map(request);
            userService.Update(user);
            updateUserResponse.Message = "User updated successfully";
            
            return Ok(updateUserResponse);
        }
        catch (Exception )
        {
            updateUserResponse.Success = false;
            updateUserResponse.Message = "There was an error updating the requested user";
            return BadRequest(updateUserResponse);
        }
    }
    
    [HttpPost("Create")]
    public IActionResult Create(CreateUserRequest request)
    {
        var createUserResponse = new CreateUserResponse { Success = true };

        try
        {
            var user = userMapper.Map(request);
            userService.Create(user);
            createUserResponse.Message = "User created successfully";
            
            return Ok(createUserResponse);
        }
        catch (Exception )
        {
            createUserResponse.Success = false;
            createUserResponse.Message = "There was an error creating the requested user";
            return BadRequest(createUserResponse);
        }
    }
    
    [HttpDelete("Delete")]
    public IActionResult Delete(long id)
    {
        var deleteUserResponse = new DeleteUserResponse { Success = true };

        try
        {
            var existingUser = userService.GetById(id);

            if (existingUser == null)
            {
                deleteUserResponse.Message = "User not found";
                return Ok(deleteUserResponse);
            }
            
            userService.Delete(existingUser);
            deleteUserResponse.Message = "User deleted successfully";
            
            return Ok(deleteUserResponse);
        }
        catch (Exception)
        {
            deleteUserResponse.Success = false;
            deleteUserResponse.Message = "There was an error deleting the requested user";
            return BadRequest(deleteUserResponse);
        }
    }
}