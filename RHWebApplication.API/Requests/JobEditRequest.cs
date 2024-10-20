namespace RHWebApplication.API.Requests;

public record JobEditRequest(int Id, string Title, string Description, int UnhealthyLevel, bool IsPericulosity, decimal OverTimeValue, decimal BaseSalary);


