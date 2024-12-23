﻿using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using RHWebApplication.Shared.Models.CompanyModels;
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
            var token = await _localStorage.GetItemAsStringAsync("jwt_token");
            if (string.IsNullOrEmpty(token))
                return _anonymous;

            var identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt_token");
            var user = new ClaimsPrincipal(identity);
            return new AuthenticationState(user);
        }
        public async Task<string?> GetClaim(string claimType)
        {
            var tokenJson = await _localStorage.GetItemAsStringAsync("jwt_token");

            if (string.IsNullOrEmpty(tokenJson))
            {
                return string.Empty;
            }

            // Deserializar o JSON para obter o token
            var tokenObj = JsonSerializer.Deserialize<Dictionary<string, string>>(tokenJson);
            if (tokenObj == null || !tokenObj.TryGetValue("Token", out var token) || string.IsNullOrEmpty(token))
            {
                throw new SecurityTokenMalformedException("Token is not well formed or missing.");
            }

            var handler = new JwtSecurityTokenHandler();
            if (!handler.CanReadToken(token))
            {
                throw new SecurityTokenMalformedException("Token is not well formed.");
            }

            var jwtToken = handler.ReadJwtToken(token);
            return jwtToken.Claims.FirstOrDefault(c => c.Type == claimType)?.Value;
        }

        public async Task Login(string token)
        {
            await _localStorage.SetItemAsStringAsync("jwt_token", token);

            var identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt_token");
            var user = new ClaimsPrincipal(identity);

            //foreach (var claim in identity.Claims)
            //{ 
            //    Console.WriteLine($"Claim Type: {claim.Type}, Claim Value: {claim.Value}");
            //}
            var userName = identity.FindFirst(ClaimTypes.Name)?.Value;

            if (!string.IsNullOrEmpty(userName))
            {
                await _localStorage.SetItemAsStringAsync("userName", userName);
            }

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("jwt_token");

            NotifyAuthenticationStateChanged(Task.FromResult(_anonymous));
        }

        private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
            var claims = keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString())).ToList();

            // Adicionar a reivindicação de nome de usuário se `unique_name` estiver presente
            if (!claims.Any(c => c.Type == ClaimTypes.Name) && keyValuePairs.ContainsKey("unique_name"))
            {
                var nameid = keyValuePairs["nameid"].ToString();
                var unique_name = keyValuePairs["unique_name"].ToString();
                var role = keyValuePairs["role"].ToString();
                var company = keyValuePairs["company"].ToString();

                if(role == "Employee")
                {
                    var jobtitle = keyValuePairs["jobtitle"].ToString();
                    if (!string.IsNullOrEmpty(jobtitle))
                        claims.Add(new Claim(ClaimTypes.NameIdentifier, jobtitle));
                }

                if (!string.IsNullOrEmpty(nameid))
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, nameid));

                if (!string.IsNullOrEmpty(unique_name))
                    claims.Add(new Claim(ClaimTypes.Name, unique_name));

                if(!string.IsNullOrEmpty(role))
                    claims.Add(new Claim(ClaimTypes.Role, role));

				if (!string.IsNullOrEmpty(company))
					claims.Add(new Claim(ClaimTypes.NameIdentifier, company));
			}
            return claims;
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
