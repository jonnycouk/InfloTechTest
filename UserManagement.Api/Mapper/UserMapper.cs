using Riok.Mapperly.Abstractions;
using UserManagement.Models;
using UserManagement.Sdk.Model;
using UserManagement.Sdk.Request.Logs;
using UserManagement.Sdk.Request.Users;

namespace UserManagement.Api.Mapper;

[Mapper]
public partial class UserMapper
{
    [MapperIgnoreTarget(nameof(User.IsActive))]
    [MapperIgnoreTarget(nameof(User.Id))]
    [MapperIgnoreTarget(nameof(User.PasswordHash))]
    [MapperIgnoreTarget(nameof(User.PasswordSalt))]
    public partial User Map(CreateUserRequest request);
    
    [MapperIgnoreSource(nameof(User.PasswordHash))]
    [MapperIgnoreSource(nameof(User.PasswordSalt))]
    public partial UserDto Map(User entity);
    
    [MapperIgnoreSource(nameof(User.Id))]
    [MapperIgnoreSource(nameof(User.IsActive))]
    public partial CreateUserRequest Map(UserDto entity);

    [MapperIgnoreTarget(nameof(User.PasswordHash))]
    [MapperIgnoreTarget(nameof(User.PasswordSalt))]
    public partial User Map(UpdateUserRequest entity);
    
    public partial List<UserDto> Map(List<User> entities);

}