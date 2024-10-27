using Microsoft.AspNetCore.Mvc;
using RHWebApplication.API.Requests;
using RHWebApplication.API.Responses;
using RHWebApplication.Database;
using RHWebApplication.Shared.Models.CompanyModels;
using RHWebApplication.Shared.Models.UserModels;

namespace RHWebApplication.API.EndPoints;

public static class EmployeesExtensions
{
    public static void AddEndPointsEmployees(this WebApplication app)
    {
        var employeeGroup = app.MapGroup("/Employee").WithTags("Employee EndPoints");

        employeeGroup.MapGet("/", async ([FromServices]DAL<User> dalUsers) =>
        {
            var employees = await dalUsers.ToListAsync<Employee>();
            var employeesResponse = new List<EmployeeResponse>();
            foreach(var employee in employees)
            {
                employeesResponse.Add(new EmployeeResponse(employee.Id, employee.Login, employee.Name, employee.Email, employee.Dependents, employee.Job.Name, employee.CreationDate, employee.TerminationDate, employee.IsActive));
            }
            return Results.Ok(employeesResponse);
        });

        employeeGroup.MapGet("/{Id:int}", async ([FromServices]DAL<User> dalUsers, int Id) =>
        {
            var employee = await dalUsers.FindByAsync<Employee>(a => a.Id == Id);
            if (employee is null)
                return Results.NotFound("Employee ID is not found!");
            var employeeResponse = new EmployeeResponse(employee.Id, employee.Login, employee.Name, employee.Email, employee.Dependents, employee.Job.Name, employee.CreationDate, employee.TerminationDate, employee.IsActive);
        return Results.Ok(employeeResponse);
        });

        employeeGroup.MapGet("/{company}", async ([FromServices] DAL<User> dalUsers, string company) =>
        {
            var employees = await dalUsers.ToListAsync<Employee>();
            var employeesResponse = new List<EmployeeResponse>();
  
            foreach (var employee in employees)
            {
                if (employee.Job.Company.Equals(company) || company == string.Empty)
                {
                    employeesResponse.Add(new EmployeeResponse(employee.Id, employee.Login, employee.Name, employee.Email, employee.Dependents, employee.Job.Name, employee.CreationDate, employee.TerminationDate, employee.IsActive));
                }
            }
            return Results.Ok(employeesResponse);
        });

        employeeGroup.MapGet("/Names/{company}", async ([FromServices] DAL<User> dalUsers, string company) =>
        {
            var employees = await dalUsers.ToListAsync<Employee>();
            var employeesResponse = new List<string>();

            foreach (var employee in employees)
            {
                if (employee.Job.Company.Equals(company) || company == string.Empty)
                {
                    employeesResponse.Add(employee.Name);
                }
            }
            return Results.Ok(employeesResponse);
        });

        employeeGroup.MapGet("/Names", async ([FromServices] DAL<Employee> dalEmployee) =>
        {
            var employees = await dalEmployee.ToListAsync();
            List<string> nameList = new List<string>();
            foreach (var employee in employees)
            {
                if (employee.Name is not null)
                    nameList.Add(employee.Name);
            }

            return Results.Ok(nameList);
        });

        employeeGroup.MapPost("/", async ([FromServices]DAL<Employee> dalEmployees, [FromServices]DAL<JobTitle> dalJobs, [FromBody]EmployeeRequest employeeRequest) =>
        {
            var job = await dalJobs.FindByAsync(a=>a.Name.Equals(employeeRequest.JobTitle));
            if (job is null)
                return Results.NotFound(employeeRequest.JobTitle + " Job Title is not found!");

            var employee = new Employee(employeeRequest.Login, employeeRequest.Password, employeeRequest.Name, employeeRequest.Email, job, employeeRequest.Dependents);
            await dalEmployees.AddAsync(employee);
            return Results.Created();
        });

        employeeGroup.MapPut("/", async ([FromServices]DAL<Employee> dalEmployees,[FromServices]DAL<JobTitle> dalJobs,[FromBody]EmployeeEditRequest employeeRequest) =>
        {
            var employee = await dalEmployees.FindByAsync(a=>a.Id == employeeRequest.Id);
            if (employee is null)
                return Results.NotFound("Employee ID is not found!");

            if(employeeRequest.JobTitle is not null)
            {
                var job = await dalJobs.FindByAsync(a => a.Name.Equals(employeeRequest.JobTitle));
                if (job == null)
                    return Results.NotFound(employeeRequest.JobTitle + " Job Title is not found!");

                employee.Job = job;
            }

            employee.Login = employeeRequest.Login;
            employee.Password = employeeRequest.Password;
            employee.Name = employeeRequest.Name;
            employee.Email = employeeRequest.Email;
            await dalEmployees.UpdateAsync(employee);
            return Results.NoContent();
        });
    }
}
