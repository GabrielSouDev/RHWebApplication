//using RHWebApplication.Web.Requests;
//using System.Net.Http.Json;

//public class AuthService
//{
//    private readonly HttpClient _httpClient;

//    public AuthService(HttpClient httpClient)
//    {
//        _httpClient = httpClient;
//    }

//    public async Task<string> LoginAsync(LoginRequest loginRequest)
//    {
//        var response = await _httpClient.PostAsJsonAsync("login", loginRequest);
//        response.EnsureSuccessStatusCode();

//        var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
//        return result.Token;
//    }
//}

//public class LoginResponse
//{
//    public string Token { get; set; }
//}
