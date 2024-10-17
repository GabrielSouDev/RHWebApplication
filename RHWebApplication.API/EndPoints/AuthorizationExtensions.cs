using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using RHWebApplication.Database;
using RHWebApplication.Shared.Models.UserModels;
using RHWebApplication.Web.Requests;
using System.Security.Principal;

public static class AuthenticationExtensions
{
    public static void AddEndPointsAuthentication(this WebApplication app)
    {
        app.MapPost("/login", async Task<IResult> ([FromServices] DAL<User> dalUser, [FromBody] LoginRequest loginRequest) =>
        {
            var user = await dalUser.FindByAsync(u => u.Login == loginRequest.Login);

            if (user != null && user.Password == loginRequest.Password)
            {
                var key = Encoding.ASCII.GetBytes("this-is-my-super-secret-mistery-key-to-create-the-token");
                var tokenHandler = new JwtSecurityTokenHandler();
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, user.Login),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                };

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
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
