using RHWebApplication.Shared.Models.UserModels;
using System.Net.Http.Json;

namespace RHWebApplication.Web.Services;

public class AuthService
{
    private readonly HttpClient _httpClient;

    public AuthService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<string> LoginAsync(User user)
    {
        var response = await _httpClient.PostAsJsonAsync("api/auth/login", user);
        response.EnsureSuccessStatusCode();

        var token = await response.Content.ReadAsStringAsync();
        return token;
    }

}
