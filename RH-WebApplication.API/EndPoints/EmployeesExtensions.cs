using Microsoft.AspNetCore.Mvc;
using RH_WebApplication.API.Requests;
using RHWebApplication.Database;
using RHWebApplication.Shared.Models.JobModels;
using RHWebApplication.Shared.Models.UserModels;

namespace RH_WebApplication.API.EndPoints;

public static class EmployeesExtensions
{
    public static void AddEndPointsEmployees(this WebApplication app)
    {
        app.MapGet("/Employee", async ([FromServices]DAL<Employee> dalEmployees) =>
        {
            return Results.Ok(await dalEmployees.ListAsync());
        });

        app.MapGet("/Employee/{Id}", async ([FromServices]DAL<User> dalUsers, [FromServices]DAL<Employee> dalEmployees, int Id) =>
        {
            var Employee = await dalUsers.FindByAsync<Employee>(a => a.Id == Id);
            if (Employee is null)
                return Results.NotFound("Employee ID is not found!");

            return Results.Ok(Employee);
        });

        app.MapPost("/Employee", async ([FromServices]DAL<Employee> dalEmployees, [FromServices]DAL<Job> dalJobs, [FromBody]EmployeeRequest employeeRequest) =>
        {
            var job = await dalJobs.FindByAsync(a=>a.Title.Equals(employeeRequest.JobTitle));
            if (job == null)
                return Results.NotFound(employeeRequest.JobTitle + " Job Title is not found!");

            var employee = new Employee(employeeRequest.login, employeeRequest.password, employeeRequest.name, employeeRequest.email, job);
            await dalEmployees.AddAsync(employee);
            return Results.Created();
        });

        app.MapPut("/Employee", async ([FromServices]DAL<Employee> dalEmployees, [FromServices]DAL<Job> dalJobs,[FromBody]EmployeeEditRequest employeeRequest) =>
        {
            var employee = await dalEmployees.FindByAsync(a=>a.Id.Equals(employeeRequest.id));
            if (employee is null)
                return Results.NotFound("Employee ID is not found!");

            var job = await dalJobs.FindByAsync(a => a.Title.Equals(employeeRequest.JobTitle));
            if (job == null)
                return Results.NotFound(employeeRequest.JobTitle + " Job Title is not found!");

            employee.Login = employeeRequest.login;
            employee.Password = employeeRequest.password;
            employee.Name = employeeRequest.name;
            employee.Email = employeeRequest.email;
            employee.Job = job;

            return Results.NoContent();
        });

        app.MapDelete("/Employee/{Id}", async ([FromServices]DAL<User> dalUsers, [FromServices]DAL<Employee>dalEmployees, int Id) =>
        {
            var employee = await dalUsers.FindByAsync<Employee>(a => a.Id == Id);
            if (employee is null)
                return Results.NotFound("Employee ID is not found!");

            await dalEmployees.DeleteAsync(employee);
            return Results.NoContent();
        });
    }
}
