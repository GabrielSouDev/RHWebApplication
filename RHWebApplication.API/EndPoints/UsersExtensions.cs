using Microsoft.AspNetCore.Mvc;
using RHWebApplication.Database;
using RHWebApplication.Shared.Models.UserModels;
using RHWebApplication.API.Responses;
using System.Linq;
using RHWebApplication.API.Requests;

namespace RHWebApplication.API.EndPoints;

public static class UsersExtensions
{
    public static void AddEndPointsUsers(this WebApplication app)
    {
        var userGroup = app.MapGroup("/User").WithTags("User EndPoints");
        userGroup.MapGet("/", async ([FromServices]DAL<User> dalUsers) =>
        {
            var users = await dalUsers.ToListAsync();
            var usersResponse = new List<UserResponse>();
            foreach (var user in users)
            {
                usersResponse.Add(new UserResponse(user.Id, user.Name, user.Login, user.Email, user.CreationDate, user.UserType, user.IsActive));
            }
            
            return Results.Ok(usersResponse);
        });

        userGroup.MapGet("/{Id}", async ([FromServices]DAL<User> dalUsers, int Id) =>
        {
            var user = await dalUsers.FindByAsync(a=>a.Id == Id);
            if (user is null)
                return Results.NotFound("User ID is not found!");

            var usersResponse = new UserResponse(user.Id,user.Name, user.Login, user.Email, user.CreationDate, user.UserType, user.IsActive);
            return Results.Ok(usersResponse);
        });

        userGroup.MapPut("/", async ([FromServices] DAL<User> dalUsers, [FromBody] UserRequest userRequest) =>
        {
            var admin = await dalUsers.FindByAsync(a => a.Id == userRequest.Id);
            if (admin is null)
                return Results.NotFound("Admin ID is not found!");

            admin.Login = userRequest.Login;
            admin.Password = userRequest.Password;
            admin.Name = userRequest.Name;
            admin.Email = userRequest.Email;

            await dalUsers.UpdateAsync(admin);
            return Results.NoContent();
        });
        userGroup.MapDelete("/{Id}", async ([FromServices] DAL<User> dalUsers, int Id) =>
        {
            var user = await dalUsers.FindByAsync(a => a.Id == Id);
            if (user is null)
                return Results.NotFound("Employee ID is not found!");

            await dalUsers.DeleteAsync(user);
            return Results.NoContent();
        });
    }
}
