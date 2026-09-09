namespace UserManagement.Sdk.Response;

public class BaseResponse
{
    public string? Message { get; set; } = null;
    public bool Success { get; set; }
}