using Microsoft.AspNetCore.Mvc;
using RHWebApplication.API.Requests;
using RHWebApplication.API.Responses;
using RHWebApplication.Database;
using RHWebApplication.Shared.Models.PayrollModels;
using RHWebApplication.Shared.Models.UserModels;
using System.Text.RegularExpressions;

namespace RHWebApplication.API.EndPoints;

public static class PayrollsExtensions
{
    public static void AddEndPointsPayrolls(this WebApplication app)
    {
        var payrollGroup = app.MapGroup("/Payroll").WithTags("Payroll EndPoints");

        payrollGroup.MapGet("/", async ([FromServices]DAL<Payroll> dalPayrolls) =>
        {
            var payrolls = await dalPayrolls.ToListAsync();
            var payrollsResponse = new List<PayrollResponse>();
            foreach (var payroll in payrolls)
            {
                payrollsResponse.Add(new PayrollResponse(payroll.Id, payroll.Employee.Name, payroll.Employee.Job.Title,
                    payroll.Employee.Job.UnhealthyLevel, payroll.Employee.Job.IsPericulosity, payroll.Employee.Job.BaseSalary, 
                    payroll.Gross, payroll.OverTime, payroll.Commission, payroll.Additionals, payroll.Deductions,
                    payroll.Net, payroll.CreationDate));
            }
            
            return Results.Ok(payrollsResponse);
        });

        payrollGroup.MapGet("/{Id}", async ([FromServices]DAL<Payroll> dalPayrolls, int Id) => 
        {
            var payroll = await dalPayrolls.FindByAsync(a => a.Id == Id);
            if (payroll is null)
                return Results.NotFound("Payroll is not found!");

            var payrollResponse = new PayrollResponse(payroll.Id, payroll.Employee.Name, payroll.Employee.Job.Title, 
            payroll.Employee.Job.UnhealthyLevel, payroll.Employee.Job.IsPericulosity, payroll.Employee.Job.BaseSalary,
            payroll.Gross, payroll.OverTime, payroll.Commission, payroll.Additionals, payroll.Deductions,
            payroll.Net, payroll.CreationDate);

            return Results.Ok(payrollResponse);
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
