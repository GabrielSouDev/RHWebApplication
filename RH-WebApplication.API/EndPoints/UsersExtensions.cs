using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RHWebApplication.Database;
using RHWebApplication.Shared.Models.UserModels;
using System.Text.Json;

namespace RH_WebApplication.API.EndPoints;

public static class UsersExtensions
{
    public static void AddEndPointsUsers(this WebApplication app)
    {
        var userGroup = app.MapGroup("/Users").WithTags("User EndPoints");
        userGroup.MapGet("/", async ([FromServices]DAL<User> dalUsers) =>
        {
            return Results.Ok(await dalUsers.ToListAsync());
        });

        userGroup.MapGet("/{Id}", async ([FromServices]DAL<User> dalUsers, int Id) =>
        {
            var user = await dalUsers.FindByAsync(a=>a.Id == Id);
            if (user is null)
                return Results.NotFound("User ID is not found!");

            return Results.Ok(user);
        });
    }
}
