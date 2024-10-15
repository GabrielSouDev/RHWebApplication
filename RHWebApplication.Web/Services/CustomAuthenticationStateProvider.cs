using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using RHWebApplication.Shared.Models.UserModels;
<<<<<<< HEAD
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace RHWebApplication.Web.Services;
=======
using System.Security.Claims;
using System.Threading.Tasks;

>>>>>>> 10dc2d2a6f97b77b35e997c3ba77477cbc4998b1
public class CustomAuthStateProvider : AuthenticationStateProvider
{
    private readonly ILocalStorageService _localStorage;
    private readonly AuthenticationState _anonymous;

    public CustomAuthStateProvider(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
        _anonymous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
<<<<<<< HEAD
        var token = await _localStorage.GetItemAsync<string>("jwt_token");

        if (string.IsNullOrEmpty(token))
=======
        var userName = await _localStorage.GetItemAsync<string>("userName");

        if (string.IsNullOrEmpty(userName))
>>>>>>> 10dc2d2a6f97b77b35e997c3ba77477cbc4998b1
        {
            return _anonymous;
        }

<<<<<<< HEAD
        var identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");
=======
        var identity = new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Name, userName)
        }, "localStorageAuth");
>>>>>>> 10dc2d2a6f97b77b35e997c3ba77477cbc4998b1

        var user = new ClaimsPrincipal(identity);
        return new AuthenticationState(user);
    }

<<<<<<< HEAD
    public async Task Login(string token)
    {
        await _localStorage.SetItemAsync("jwt_token", token);

        var identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");
=======
    public async Task Login(User userLogin)
    {
        var userName = userLogin.Login;
        await _localStorage.SetItemAsync("userName", userName);

        var identity = new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Name, userName)
        }, "localStorageAuth");
>>>>>>> 10dc2d2a6f97b77b35e997c3ba77477cbc4998b1

        var user = new ClaimsPrincipal(identity);
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
    }

    public async Task Logout()
    {
<<<<<<< HEAD
        await _localStorage.RemoveItemAsync("jwt_token");

        NotifyAuthenticationStateChanged(Task.FromResult(_anonymous));
    }

    private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        var payload = jwt.Split('.')[1];
        var jsonBytes = ParseBase64WithoutPadding(payload);
        var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

        return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
    }

    private byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2: base64 += "=="; break;
            case 3: base64 += "="; break;
        }
        return Convert.FromBase64String(base64);
    }
}
=======
        await _localStorage.RemoveItemAsync("userName");

        NotifyAuthenticationStateChanged(Task.FromResult(_anonymous));
    }
    public async Task<string?> GetUsernameAsync()
    {
        return await _localStorage.GetItemAsync<string>("userName");
    }

}
>>>>>>> 10dc2d2a6f97b77b35e997c3ba77477cbc4998b1
