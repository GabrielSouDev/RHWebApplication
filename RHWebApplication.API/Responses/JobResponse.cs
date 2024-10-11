namespace RHWebApplication.API.Responses;

public record JobResponse(int Id, string Title, string Description, bool IsUnhealthy, bool IsPericulosity, decimal BaseSalary);