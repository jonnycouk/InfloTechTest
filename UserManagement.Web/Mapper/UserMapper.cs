using Riok.Mapperly.Abstractions;
using UserManagement.Sdk.Model;
using UserManagement.Sdk.Request.Users;
using UserManagement.Web.Models.Users;

namespace UserManagement.Web.Mapper;

[Mapper]
public partial class UserMapper
{
    [MapperIgnoreSource(nameof(UserDto.Id))]
    [MapperIgnoreSource(nameof(UserDto.IsActive))]
    public partial CreateUserRequest MapToCreateRequest(UserDto entity);
    public partial UpdateUserRequest MapToUpdateRequest(UserDto entity);
    
    public partial UserListItemViewModel Map(UserDto entity);
    
    public partial List<UserListItemViewModel> Map(List<UserDto> entities);
}