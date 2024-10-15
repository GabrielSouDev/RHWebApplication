using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RHWebApplication.API.Requests;
using RHWebApplication.Database;
using RHWebApplication.Shared.Models.UserModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Authorization;

public static class AuthenticationExtensions
{
    public static void AddEndPointsAuthentication(this WebApplication app)
    {
        app.MapPost("/login", async Task<IResult> ([FromServices] DAL<User> dalUser, [FromBody] LoginRequest loginRequest) =>
        {
            var user = await dalUser.FindByAsync(u => u.Login == loginRequest.Login);

            if (user is null)
                return Results.Unauthorized();
            if( user.Password.Equals(loginRequest.Password))
            {
                // Gera o token JWT
                var token = GenerateJwtToken(user);
                return Results.Ok(new { Token = token });
            }
            return Results.Unauthorized();
        });
    }
    private static string GenerateJwtToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes("this-is-my-super-secret-mistery-key-to-create-the-token");

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.Login),
                new Claim(ClaimTypes.Role, "user") // ou o papel adequado
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}