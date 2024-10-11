using Microsoft.AspNetCore.Mvc;
using RHWebApplication.Database;
using RHWebApplication.Shared.Models.UserModels;
using RHWebApplication.API.Responses;
using System.Linq;

namespace RHWebApplication.API.EndPoints;

public static class UsersExtensions
{
    public static void AddEndPointsUsers(this WebApplication app)
    {
        var userGroup = app.MapGroup("/Users").WithTags("User EndPoints");
        userGroup.MapGet("/", async ([FromServices]DAL<User> dalUsers) =>
        {
            var users = await dalUsers.ToListAsync();
            var usersResponse = new List<UserResponse>();
            foreach (var user in users)
            {
                usersResponse.Add(new UserResponse(user.Id, user.Name, user.Login, user.Email, user.CreationDate, user.IsActive));
            }
            
            return Results.Ok(usersResponse);
        });

        userGroup.MapGet("/{Id}", async ([FromServices]DAL<User> dalUsers, int Id) =>
        {
            var user = await dalUsers.FindByAsync(a=>a.Id == Id);
            if (user is null)
                return Results.NotFound("User ID is not found!");

            var usersResponse = new UserResponse(user.Id,user.Name, user.Login, user.Email, user.CreationDate, user.IsActive);
            return Results.Ok(usersResponse);
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
