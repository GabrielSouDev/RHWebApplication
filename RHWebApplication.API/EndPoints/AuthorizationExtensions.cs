using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RHWebApplication.API.Requests;
using RHWebApplication.Database;
using RHWebApplication.Shared.Models.UserModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;

public class AuthenticationExtensions
{

    [HttpPost("login")]
    public async Task<IResult> Login([FromServices] DAL<User> dalUser, [FromBody] LoginRequest loginRequest)
    {
        var user = await dalUser.FindByAsync<User>(u => u.Login == loginRequest.Login && u.Password == loginRequest.Password);

        if (user == null)
            return Unauthorized;

        // Gera o token JWT
        var token = GenerateJwtToken(user);
        return Results.Ok(new { Token = token });
    }

    private string GenerateJwtToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes("its-my-secret-key");

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.Login),
                new Claim(ClaimTypes.Role, "Admin") // ou o papel adequado
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}