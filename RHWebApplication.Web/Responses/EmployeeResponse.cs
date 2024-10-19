namespace RHWebApplication.Web.Responses;

public record EmployeeResponse(int Id, string Name, string JobTitle, string Login, string Email, 
    DateTime CreationDate, DateTime? TerminationDate, bool IsActive);