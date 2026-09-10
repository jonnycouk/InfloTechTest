using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using UserManagement.Sdk.Model;
using UserManagement.Sdk.Request.Users;
using UserManagement.Sdk.Response.Users;

namespace UserManagement.ApiServices.Users;

public class UserApiService(IConfiguration configuration) : IUserApiService 
{
    public IEnumerable<UserDto> GetAll(string filter)
    {
        if (filter == null) throw new ArgumentNullException(nameof(filter));
        if (filter == null) throw new ArgumentNullException(nameof(filter));
        var request = new RestRequest($"v1/user/getAll/{filter}", Method.Get);
        var client = new RestClient(configuration["ApiBaseUrl"]!);
        request.AddHeader("x-api-key", configuration["ApiKey"]!);
        request.AddQueryParameter("filter", filter);
    
        var response = client.Execute<GetUserResponse>(request);

        if (response?.StatusCode == System.Net.HttpStatusCode.OK && response.Content != null)
        {
            var getUsersResponse = JsonConvert.DeserializeObject<GetUserResponse>(response.Content);
            return getUsersResponse?.Users ?? new List<UserDto>();
        }

        return new List<UserDto>();
    }

    public void Create(CreateUserRequest createUserRequest)
    {
        var request = new RestRequest($"v1/user/create", Method.Post);
        var client = new RestClient(configuration["ApiBaseUrl"]!);
        request.AddHeader("x-api-key", configuration["ApiKey"]!);
        request.AddBody(createUserRequest);
    
        client.Execute<CreateUserResponse>(request);
    }

    public UserDto? GetById(long id)
    {
        var request = new RestRequest($"v1/user/get/{id}", Method.Get);
        var client = new RestClient(configuration["ApiBaseUrl"]!);
        request.AddHeader("x-api-key", configuration["ApiKey"]!);
        request.AddQueryParameter("id", id);
    
        var response = client.Execute<GetUserResponse>(request);

        if (response?.StatusCode == System.Net.HttpStatusCode.OK && response.Content != null)
        {
            var getUserResponse = JsonConvert.DeserializeObject<GetUserResponse>(response.Content);
            return getUserResponse?.Users[0];
        }

        return new UserDto();
    }

    public void Update(UpdateUserRequest updateUserRequest)
    {
        var request = new RestRequest("v1/user/update", Method.Put);
        var client = new RestClient(configuration["ApiBaseUrl"]!);
        request.AddHeader("x-api-key", configuration["ApiKey"]!);
        request.AddJsonBody(updateUserRequest);
        client.Execute<UpdateUserRequest>(request);
    }

    public void Delete(long id)
    {
        var request = new RestRequest($"v1/user/delete/{id}", Method.Delete);
        var client = new RestClient(configuration["ApiBaseUrl"]!);
        request.AddHeader("x-api-key", configuration["ApiKey"]!);
        request.AddQueryParameter("id", id);
        client.Execute<GetUserResponse>(request);
    }
}