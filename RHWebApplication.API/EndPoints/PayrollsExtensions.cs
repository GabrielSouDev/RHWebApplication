using Microsoft.AspNetCore.Mvc;
using RHWebApplication.API.Requests;
using RHWebApplication.API.Responses;
using RHWebApplication.Database;
using RHWebApplication.Shared.Models.CompanyModels;
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
                payrollsResponse.Add(new PayrollResponse(payroll.Id, payroll.Employee.Name, payroll.Employee.Job.Name, payroll.Employee.Job.UnhealthyLevel, payroll.Employee.Job.IsPericulosity,
	payroll.OverTime, payroll.OverTimeAditionals, payroll.PericulosityValue, payroll.UnhealthyValue,
	payroll.Commission, payroll.INSSDeduction, payroll.IRRFDeduction, payroll.Deductions, payroll.Employee.Job.BaseSalary, payroll.Net,
	payroll.Gross, payroll.CreationDate));
			}
            
            return Results.Ok(payrollsResponse);
        });

        payrollGroup.MapGet("/{Id:int}", async ([FromServices]DAL<Payroll> dalPayrolls, int Id) => 
        {
            var payroll = await dalPayrolls.FindByAsync(a => a.Id == Id);
            if (payroll is null)
                return Results.NotFound("Payroll is not found!");

            var payrollResponse = new PayrollResponse(payroll.Id, payroll.Employee.Name, payroll.Employee.Job.Name, payroll.Employee.Job.UnhealthyLevel, payroll.Employee.Job.IsPericulosity,
	payroll.OverTime, payroll.OverTimeAditionals, payroll.PericulosityValue, payroll.UnhealthyValue,
	payroll.Commission, payroll.INSSDeduction, payroll.IRRFDeduction, payroll.Deductions, payroll.Employee.Job.BaseSalary, payroll.Net,
	payroll.Gross, payroll.CreationDate);

		return Results.Ok(payrollResponse);
        });

        payrollGroup.MapGet("/{company}", async ([FromServices] DAL<Payroll> dalPayrolls, string company) =>
        {
            var payrolls = await dalPayrolls.ToListAsync();
            var payrollsResponse = new List<PayrollResponse>();
            foreach (var payroll in payrolls)
            {
                if(payroll.Employee.Job.Company.CorporateName.Equals(company) || company == string.Empty)
                {
                    payrollsResponse.Add(new PayrollResponse(payroll.Id, payroll.Employee.Name,     payroll.Employee.Job.Name,payroll.Employee.Job.UnhealthyLevel, payroll.Employee.Job.IsPericulosity,
                    payroll.OverTime, payroll.OverTimeAditionals, payroll.PericulosityValue, payroll.UnhealthyValue,
                    payroll.Commission, payroll.INSSDeduction, payroll.IRRFDeduction, payroll.Deductions,   payroll.Employee.Job.BaseSalary, payroll.Net,
                    payroll.Gross, payroll.CreationDate));
                }
            }

            return Results.Ok(payrollsResponse);
        });

        payrollGroup.MapPost("/", async ([FromServices]DAL<Payroll> dalPayrolls, [FromServices]DAL<User> dalUsers, [FromBody]PayrollRequest payrollRequest) => 
        {
            var employee = await dalUsers.FindByAsync<Employee>(a => a.Id.Equals(payrollRequest.EmployeeId));
            if (employee is null)
                return Results.NotFound("Employee is not found!");

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
