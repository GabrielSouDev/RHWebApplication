namespace RH_WebApplication.API.Requests;

public record JobEditRequest(int Id, string Title, string Description, bool IsUnhealthy, bool IsPericulosity, decimal BaseSalary);


