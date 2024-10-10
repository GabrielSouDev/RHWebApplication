using Microsoft.AspNetCore.Mvc;
using RH_WebApplication.API.Requests;
using RH_WebApplication.API.Responses;
using RHWebApplication.Database;
using RHWebApplication.Shared.Models.JobModels;
using RHWebApplication.Shared.Models.UserModels;

namespace RH_WebApplication.API.EndPoints;

public static class EmployeesExtensions
{
    public static void AddEndPointsEmployees(this WebApplication app)
    {
        var employeeGroup = app.MapGroup("/Employee").WithTags("Employee EndPoints");

        employeeGroup.MapGet("/", async ([FromServices]DAL<Employee> dalEmployees) =>
        {
            var employees = await dalEmployees.ToListAsync();
            var employeeResponse = new List<EmployeeResponse>();
            foreach(var employee in employees)
            {
                employeeResponse.Add(new EmployeeResponse(employee.Id, employee.Name, employee.Job.Title, employee.Login, employee.Email, 
                    employee.CreationDate, employee.TerminationDate, employee.IsActive));
            }
            return Results.Ok(employeeResponse);
        });

        employeeGroup.MapGet("/{Id}", async ([FromServices]DAL<User> dalUsers, int Id) =>
        {
            var employee = await dalUsers.FindByAsync<Employee>(a => a.Id == Id);
            if (employee is null)
                return Results.NotFound("Employee ID is not found!");
            var employeeResponse = new EmployeeResponse(employee.Id, employee.Name, employee.Job.Title, employee.Login, employee.Email,
                    employee.CreationDate, employee.TerminationDate, employee.IsActive);
        return Results.Ok(employeeResponse);
        });

        employeeGroup.MapPost("/", async ([FromServices]DAL<Employee> dalEmployees, [FromServices]DAL<Job> dalJobs, [FromBody]EmployeeRequest employeeRequest) =>
        {
            var job = await dalJobs.FindByAsync(a=>a.Title.Equals(employeeRequest.JobTitle));
            if (job is null)
                return Results.NotFound(employeeRequest.JobTitle + " Job Title is not found!");

            var employee = new Employee(employeeRequest.Login, employeeRequest.Password, employeeRequest.Name, employeeRequest.Email, job);
            await dalEmployees.AddAsync(employee);
            return Results.Created();
        });

        employeeGroup.MapPut("/", async ([FromServices]DAL<Employee> dalEmployees, [FromServices]DAL<User> dalUsers ,[FromServices]DAL<Job> dalJobs,[FromBody]EmployeeEditRequest employeeRequest) =>
        {
            var employee = await dalUsers.FindByAsync<Employee>(a=>a.Id == employeeRequest.Id);
            if (employee is null)
                return Results.NotFound("Employee ID is not found!");

            var job = await dalJobs.FindByAsync(a => a.Title.Equals(employeeRequest.JobTitle));
            if (job == null)
                return Results.NotFound(employeeRequest.JobTitle + " Job Title is not found!");

            employee.Login = employeeRequest.Login;
            employee.Password = employeeRequest.Password;
            employee.Name = employeeRequest.Name;
            employee.Email = employeeRequest.Email;
            employee.Job = job;
            await dalEmployees.UpdateAsync(employee);
            return Results.NoContent();
        });

        employeeGroup.MapDelete("/{Id}", async ([FromServices]DAL<User> dalUsers, [FromServices]DAL<Employee>dalEmployees, int Id) =>
        {
            var employee = await dalUsers.FindByAsync<Employee>(a => a.Id == Id);
            if (employee is null)
                return Results.NotFound("Employee ID is not found!");

            await dalEmployees.DeleteAsync(employee);
            return Results.NoContent();
        });
    }
}
