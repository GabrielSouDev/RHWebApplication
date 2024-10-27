namespace RHWebApplication.Web.Services;

using RHWebApplication.Shared.Models.UserModels;
using RHWebApplication.Web.Requests;
using RHWebApplication.Web.Responses;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

public class UserService
{
    private readonly HttpClient _httpClient;

    public UserService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<List<UserResponse>?> GetUsers()
    {
        var Response = await _httpClient.GetFromJsonAsync<List<UserResponse>>("User");
        return Response;
    }

    public async Task<List<UserResponse>?> GetUsersByCompany(string company)
    {
        var Response = await _httpClient.GetFromJsonAsync<List<UserResponse>>($"User/{company}");
        return Response;
    }

    public async Task<HttpResponseMessage?> PutUser(UserRequest user)
    {
        var Response = await _httpClient.PutAsJsonAsync("/User", user);
        return Response;
    }
    public async Task<HttpResponseMessage> DeleteUser(int id)
    {
        var Response = await _httpClient.DeleteAsync($"/User/{id}");
        return Response;
    }
}
