namespace RHWebApplication.Web.Requests;

public record JobEditRequest(int Id, string Title, string Description, bool IsUnhealthy, int UnhealthyLevel, decimal OverTimeValue, decimal BaseSalary);


