using Microsoft.AspNetCore.Mvc;
using RHWebApplication.Database;
using RHWebApplication.Shared.Models.UserModels;

namespace RH_WebApplication.API.EndPoints;

public static class UsersExtensions
{
    public static void AddEndPointsUsers(this WebApplication app)
    {
        app.MapGet("/Users", async ([FromServices]DAL<User>dalUsers) =>
        {
            return Results.Ok(await dalUsers.ListAsync());
        });
    }
}
