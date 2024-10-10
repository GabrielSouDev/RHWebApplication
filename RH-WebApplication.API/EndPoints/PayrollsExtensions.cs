using Microsoft.AspNetCore.Mvc;
using RH_WebApplication.API.Requests;
using RHWebApplication.Database;
using RHWebApplication.Shared.Models.PayrollModels;
using RHWebApplication.Shared.Models.UserModels;

namespace RH_WebApplication.API.EndPoints;

public static class PayrollsExtensions
{
    public static void AddEndPointsPayrolls(this WebApplication app)
    {
        var payrollGroup = app.MapGroup("/Payrolls").WithTags("Payroll EndPoints");

        payrollGroup.MapGet("/", async ([FromServices]DAL<Payroll> dalPayrolls) =>
        {
            return Results.Ok(await dalPayrolls.ToListAsync());
        });

        payrollGroup.MapGet("/{Id}", ([FromServices]DAL<Payroll> dalPayrolls, int Id) => 
        {
            var payroll = dalPayrolls.FindByAsync(a => a.Id == Id);
            if (payroll is null)
                return Results.NotFound("Payroll is not found!");

            return Results.Ok(payroll);
        });

        payrollGroup.MapPost("/", async ([FromServices]DAL<Payroll> dalPayrolls, [FromServices]DAL<User> dalUsers, [FromBody]PayrollRequest payrollRequest) => 
        {
            var employee = await dalUsers.FindByAsync<Employee>(a => a.Name.Equals(payrollRequest.EmployeeName));
            if (employee is null)
                return Results.NotFound("Employee name is not found!");

            await dalPayrolls.AddAsync(new Payroll(employee, payrollRequest.OverTime, payrollRequest.Commission));
            return Results.Created();
        });

        payrollGroup.MapDelete("/{Id}", async ([FromServices]DAL<Payroll> dalPayrolls, int Id) =>
        {
            var payroll = await dalPayrolls.FindByAsync(a => a.Id == Id);
            if(payroll is null)
                return Results.NotFound("Payroll is not found!");

            await dalPayrolls.DeleteAsync(payroll);
            return Results.NoContent();
        });
    }
}
