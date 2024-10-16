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
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RHWebApplication.Web.Requests;

public static class AuthenticationExtensions
{
    public static void AddEndPointsAuthentication(this WebApplication app)
    {
        app.MapPost("/login", async Task<IResult> ([FromServices]DAL<User> dalUser, [FromBody]LoginRequest loginRequest) =>
        {
            var user = await dalUser.FindByAsync(u => u.Login == loginRequest.Login);
            if (user.Password == loginRequest.Password)
            {
                var key = Encoding.ASCII.GetBytes("this-is-my-super-secret-mistery-key-to-create-the-token");
                var tokenHandler = new JwtSecurityTokenHandler();
                Console.WriteLine("loginrequest: " + loginRequest.Login);
                Console.WriteLine("user: " + user.Login);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, loginRequest.Login) }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                return Results.Ok(new { Token = tokenString });
            }
            return Results.Unauthorized();
        });
    }
}


    