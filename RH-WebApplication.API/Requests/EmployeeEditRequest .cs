namespace RH_WebApplication.API.Requests;

public record EmployeeEditRequest(int id, string login, string password, string name, string email, string JobTitle);