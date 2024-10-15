namespace RHWebApplication.Web.Requests;

public record JobRequest(string Title, string Description, bool IsUnhealthy, bool IsPericulosity, decimal BaseSalary);


