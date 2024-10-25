namespace RHWebApplication.API.Requests;

public record JobRequest(string Title, string Description, int UnhealthyLevel, bool IsPericulosity, decimal OverTimeValue, decimal BaseSalary, string CompanyName);


