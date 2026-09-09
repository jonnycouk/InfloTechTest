using UserManagement.Sdk.Model;

namespace UserManagement.Sdk.Response;

public class GetUserResponse : BaseResponse
{
    public List<UserDto>? Users { get; set; }
    public int Count { get; set; }
}
