using System.Collections.Generic;
using Riok.Mapperly.Abstractions;
using UserManagement.Sdk.Model;
using UserManagement.Web.Models.Users;

namespace UserManagement.Web.Mapper;

[Mapper]
public partial class UserMapper
{
    private partial UserListItemViewModel MapToViewModelInternal(UserDto entity);
    private partial List<UserListItemViewModel> MapToViewModelInternal(List<UserDto> entities);
    
    [UserMapping(Default = false)]
    public UserListItemViewModel Map(UserDto entity, bool includeCredentials = false)
    {
        UserListItemViewModel model = MapToViewModelInternal(entity);

        if (!includeCredentials)
        {
            model.PasswordSalt = string.Empty;
            model.PasswordHash = string.Empty;
        }

        return model;
    }
    
    [UserMapping(Default = false)]
    public List<UserListItemViewModel> Map(List<UserDto> entities, bool includeCredentials = false)
    {
        List<UserListItemViewModel> list = MapToViewModelInternal(entities);

        if (!includeCredentials)    
        {
            foreach (UserListItemViewModel item in list)
            {
                item.PasswordSalt = string.Empty;
                item.PasswordHash = string.Empty;
            }
        }

        return list;
    }
}