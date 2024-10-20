namespace RHWebApplication.API.Responses;

public record EmployeeResponse(int Id, string Name, int Dependents, string JobTitle, string Login, string Email, 
    DateTime CreationDate, DateTime? TerminationDate, bool IsActive);