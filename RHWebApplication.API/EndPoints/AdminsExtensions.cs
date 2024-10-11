using Microsoft.AspNetCore.Mvc;
using RHWebApplication.API.Responses;
using RHWebApplication.API.Requests;
using RHWebApplication.Database;
using RHWebApplication.Shared.Models.UserModels;

namespace RHWebApplication.API.EndPoints;

public static class AdminsExtensions
{
    public static void AddEndPointsAdmins(this WebApplication app)
    {
        var adminGroup = app.MapGroup("/Admin").WithTags("Admin EndPoints");

        adminGroup.MapGet("/", async ([FromServices] DAL<User> dalUser) =>
        {
            var admins = await dalUser.ToListAsync<Admin>();
            var adminsResponse = new List<AdminResponse>();
            foreach (var admin in admins)
            {
                adminsResponse.Add(new AdminResponse(admin.Id, admin.Name, admin.Login, admin.Email,
                    admin.CreationDate, admin.IsActive));
            }
            return Results.Ok(adminsResponse);
        });

        adminGroup.MapGet("/{Id}", async ([FromServices] DAL<User> dalUsers, int Id) =>
        {
            var admin = await dalUsers.FindByAsync<Admin>(a => a.Id == Id);
            if (admin is null)
                return Results.NotFound("Admin ID is not found!");
            var adminResponse = new AdminResponse(admin.Id, admin.Name, admin.Login, admin.Email,
                    admin.CreationDate, admin.IsActive);
            return Results.Ok(adminResponse);
        });

        adminGroup.MapPost("/", async ([FromServices] DAL<Admin> dalAdmins, [FromBody] AdminRequest adminRequest) =>
        {
            var admin = new Admin(adminRequest.Login, adminRequest.Password, adminRequest.Name, adminRequest.Email);
            await dalAdmins.AddAsync(admin);
            return Results.Created();
        });

        adminGroup.MapPut("/", async ([FromServices] DAL<Admin> dalAdmins, [FromBody] AdminEditRequest AdminRequest) =>
        {
            var admin = await dalAdmins.FindByAsync(a => a.Id == AdminRequest.Id);
            if (admin is null)
                return Results.NotFound("Admin ID is not found!");

            admin.Login = AdminRequest.Login;
            admin.Password = AdminRequest.Password;
            admin.Name = AdminRequest.Name;
            admin.Email = AdminRequest.Email;

            await dalAdmins.UpdateAsync(admin);
            return Results.NoContent();
        });
    }
}