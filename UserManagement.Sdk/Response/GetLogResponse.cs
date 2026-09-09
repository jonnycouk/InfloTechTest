using UserManagement.Sdk.Model;

namespace UserManagement.Sdk.Response;

public class GetLogResponse : BaseResponse
{
    public List<LogDto>? Logs { get; set; }
    public int Count { get; set; }
}