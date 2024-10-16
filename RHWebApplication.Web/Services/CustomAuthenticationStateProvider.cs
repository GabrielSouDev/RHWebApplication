using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.IdentityModel.Tokens;
using RHWebApplication.Shared.Models.UserModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace RHWebApplication.Web.Services
{
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
            var token = await _localStorage.GetItemAsync<string>("jwt_token");

            if (string.IsNullOrEmpty(token))
                return _anonymous;

            var identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt_token");
            var user = new ClaimsPrincipal(identity);
            return new AuthenticationState(user);
        }

        public async Task Login(string token)
        {
            await _localStorage.SetItemAsync("jwt_token", token);

            var identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt_token");
            var user = new ClaimsPrincipal(identity);

            // Armazenar o nome de usuário
            var userName = identity.FindFirst(ClaimTypes.Name)?.Value;

            if (!string.IsNullOrEmpty(userName))
            {
                await _localStorage.SetItemAsync("userName", userName);
            }

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("jwt_token");
            await _localStorage.RemoveItemAsync("userName");

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
}
