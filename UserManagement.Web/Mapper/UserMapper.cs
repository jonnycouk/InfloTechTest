using Riok.Mapperly.Abstractions;
using UserManagement.Models;
using UserManagement.Web.Models.Users;

namespace UserManagement.Web.Mapper;

[Mapper]
public partial class UserMapper
{
    public partial UserListItemViewModel Map(User entity);
    public partial List<UserListItemViewModel> Map(List<User> entities);
}