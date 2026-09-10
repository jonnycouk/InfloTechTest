using UserManagement.Sdk.Model;

namespace UserManagement.Sdk.Response.Users;

public class GetUserResponse : BaseResponse
{
    public List<UserDto> Users { get; set; } = new();
    public int Count { get; set; }
}
