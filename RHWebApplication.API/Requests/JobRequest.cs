namespace RHWebApplication.API.Requests;

public record JobRequest(string Title, string Description, bool IsUnhealthy, bool IsPericulosity, decimal BaseSalary);


