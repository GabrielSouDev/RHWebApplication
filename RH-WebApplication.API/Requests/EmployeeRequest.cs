namespace RH_WebApplication.API.Requests;

public record EmployeeRequest(string Login, string Password, string Name, string Email, string  JobTitle);