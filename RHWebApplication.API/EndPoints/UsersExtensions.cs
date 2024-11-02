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
                usersResponse.Add(new UserResponse(user.Id, user.Name, user.Login, user.Email, user.CreationDate, user.UserType, user.Company.TradeName, user.IsActive));
            }
            
            return Results.Ok(usersResponse);
        });

        userGroup.MapGet("/{company}", async ([FromServices] DAL<User> dalUsers, string company) =>
        {
            var users = await dalUsers.ToListAsync();
            var usersResponse = new List<UserResponse>();
            foreach (var user in users)
            {
                if(user.Company.TradeName == company || company == string.Empty)
                    usersResponse.Add(new UserResponse(user.Id, user.Name, user.Login, user.Email, user.CreationDate, user.UserType, user.Company.TradeName,
                        user.IsActive));
            }

            return Results.Ok(usersResponse);
        });

        userGroup.MapGet("/{Id:int}", async ([FromServices]DAL<User> dalUsers, int Id) =>
        {
            var user = await dalUsers.FindByAsync(a=>a.Id == Id);
            if (user is null)
                return Results.NotFound("User ID is not found!");

            var usersResponse = new UserResponse(user.Id,user.Name, user.Login, user.Email, user.CreationDate, user.UserType, user.Company.TradeName, user.IsActive);
            return Results.Ok(usersResponse);
        });

        userGroup.MapPut("/", async ([FromServices] DAL<User> dalUsers, [FromBody] UserRequest userRequest) =>
        {
            var user = await dalUsers.FindByAsync(a => a.Id == userRequest.Id);
            if (user is null)
                return Results.NotFound("Admin ID is not found!");
            user.Login = userRequest.Login;
            user.Password = userRequest.Password;
            user.Name = userRequest.Name;
            user.Email = userRequest.Email;

            await dalUsers.UpdateAsync(user);
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
