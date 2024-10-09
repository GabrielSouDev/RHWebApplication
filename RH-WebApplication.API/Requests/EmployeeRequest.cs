namespace RH_WebApplication.API.Requests;

public record EmployeeRequest(string login, string password, string name, string email, string JobTitle);