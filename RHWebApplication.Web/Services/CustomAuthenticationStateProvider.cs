using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using RHWebApplication.Shared.Models.UserModels;
using System.Security.Claims;
using System.Threading.Tasks;

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
        var userName = await _localStorage.GetItemAsync<string>("userName");

        if (string.IsNullOrEmpty(userName))
        {
            return _anonymous;
        }

        var identity = new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Name, userName)
        }, "localStorageAuth");

        var user = new ClaimsPrincipal(identity);
        return new AuthenticationState(user);
    }

    public async Task Login(User userLogin)
    {
        var userName = userLogin.Login;
        await _localStorage.SetItemAsync("userName", userName);

        var identity = new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Name, userName)
        }, "localStorageAuth");

        var user = new ClaimsPrincipal(identity);
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
    }

    public async Task Logout()
    {
        await _localStorage.RemoveItemAsync("userName");

        NotifyAuthenticationStateChanged(Task.FromResult(_anonymous));
    }
    public async Task<string?> GetUsernameAsync()
    {
        return await _localStorage.GetItemAsync<string>("userName");
    }

}
