namespace RHWebApplication.API.Requests;
public record EmployeeEditRequest(int Id, string Login, string Password, string Name, string Email, string JobTitle);