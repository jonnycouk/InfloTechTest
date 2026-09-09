using Riok.Mapperly.Abstractions;
using UserManagement.Models;
using UserManagement.Sdk.Model;
using UserManagement.Sdk.Request;

namespace UserManagement.Api.Mapper;

[Mapper]
public partial class UserMapper
{
    [MapperIgnoreTarget(nameof(User.PasswordHash))]
    [MapperIgnoreTarget(nameof(User.PasswordSalt))]
    public partial User Map(UpdateUserRequest request);
    
    [MapperIgnoreTarget(nameof(User.Id))]
    [MapperIgnoreTarget(nameof(User.IsActive))]
    public partial User Map(CreateUserRequest request);
    
    [UserMapping(Default = false)]
    public UserDto Map(User entity, bool includeCredentials = false)
    {
        UserDto model = MapToViewModelInternal(entity);

        if (!includeCredentials)
        {
            model.PasswordSalt = string.Empty;
            model.PasswordHash = string.Empty;
        }

        return model;
    }
    
    [UserMapping(Default = false)]
    public List<UserDto> Map(List<User> entities, bool includeCredentials = false)
    {
        List<UserDto> list = MapToViewModelInternal(entities);

        if (!includeCredentials)    
        {
            foreach (UserDto item in list)
            {
                item.PasswordSalt = string.Empty;
                item.PasswordHash = string.Empty;
            }
        }

        return list;
    }
    
    
    
    private partial UserDto MapToViewModelInternal(User entity);
    private partial List<UserDto> MapToViewModelInternal(List<User> entities);

}