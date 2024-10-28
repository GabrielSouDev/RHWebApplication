using Microsoft.AspNetCore.Mvc;
using RHWebApplication.API.Responses;
using RHWebApplication.API.Requests;
using RHWebApplication.Database;
using RHWebApplication.Shared.Models.UserModels;
using RHWebApplication.Shared.Models.CompanyModels;

namespace RHWebApplication.API.EndPoints;

public static class StaffsExtensions
{
    public static void AddEndPointsStaffs(this WebApplication app)
    {
        var StaffGroup = app.MapGroup("/Staff").WithTags("Staff EndPoints");

        StaffGroup.MapGet("/", async ([FromServices] DAL<User> dalUser) =>
        {
            var Staffs = await dalUser.ToListAsync<Staff>();
            var StaffsResponse = new List<StaffResponse>();
            foreach (var Staff in Staffs)
            {
                StaffsResponse.Add(new StaffResponse(Staff.Id, Staff.Name, Staff.Login, Staff.Email,
                    Staff.CreationDate, Staff.IsActive));
            }
            return Results.Ok(StaffsResponse);
        });

        StaffGroup.MapGet("/{Id}", async ([FromServices] DAL<User> dalUsers, int Id) =>
        {
            var Staff = await dalUsers.FindByAsync<Staff>(a => a.Id == Id);
            if (Staff is null)
                return Results.NotFound("Staff ID is not found!");
            var StaffResponse = new StaffResponse(Staff.Id, Staff.Name, Staff.Login, Staff.Email,
                    Staff.CreationDate, Staff.IsActive);
            return Results.Ok(StaffResponse);
        });

        StaffGroup.MapPost("/", async ([FromServices] DAL<Staff> dalStaffs, [FromServices] DAL<Company> dalCompanys, [FromBody] StaffRequest StaffRequest) =>
        {
            var company = await dalCompanys.FindByAsync(c => c.CorporateName == StaffRequest.CompanyName);
            if (company is not null)
            {
                var Staff = new Staff(StaffRequest.Login, StaffRequest.Password, StaffRequest.Name, StaffRequest.Email, company.Id);
                await dalStaffs.AddAsync(Staff);
                return Results.Created();
            }
            return Results.NotFound("Company Name is not found!");
        });

        StaffGroup.MapPut("/", async ([FromServices] DAL<Staff> dalStaffs, [FromBody] StaffEditRequest StaffRequest) =>
        {
            var Staff = await dalStaffs.FindByAsync(a => a.Id == StaffRequest.Id);
            if (Staff is null)
                return Results.NotFound("Staff ID is not found!");

            Staff.Login = StaffRequest.Login;
            Staff.Password = StaffRequest.Password;
            Staff.Name = StaffRequest.Name;
            Staff.Email = StaffRequest.Email;

            await dalStaffs.UpdateAsync(Staff);
            return Results.NoContent();
        });
    }
}