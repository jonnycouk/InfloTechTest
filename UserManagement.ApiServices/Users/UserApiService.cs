using Newtonsoft.Json;
using RestSharp;
using UserManagement.Sdk.Model;
using UserManagement.Sdk.Response.Users;

namespace UserManagement.ApiServices.Users;

public class UserApiService : IUserApiService 
{
    public IEnumerable<UserDto> FilterByActive(bool isActive)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<UserDto> GetAll(string filter)
    {
        var client = new RestClient("https://inflo-api.jonny.uk");
        var request = new RestRequest($"v1/user/getAll/{filter}", Method.Get);
        request.AddQueryParameter("filter", filter);
        request.AddHeader("x-api-key", "jonnys-new-job");
    
        var response = client.Execute<GetUserResponse>(request);

        if (response?.StatusCode == System.Net.HttpStatusCode.OK && response.Content != null)
        {
            var getUsersResponse = JsonConvert.DeserializeObject<GetUserResponse>(response.Content);
            return getUsersResponse?.Users ?? new List<UserDto>();
        }

        return new List<UserDto>();
    }

    public void Create(UserDto user)
    {
        throw new NotImplementedException();
    }

    public UserDto? GetById(long id)
    {
        throw new NotImplementedException();
    }

    public UserDto? GetDetachedEntityById(long id)
    {
        throw new NotImplementedException();
    }

    public void Update(UserDto user)
    {
        throw new NotImplementedException();
    }

    public void Delete(UserDto user)
    {
        throw new NotImplementedException();
    }
}