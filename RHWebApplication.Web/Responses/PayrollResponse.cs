namespace RHWebApplication.API.Responses;

public record PayrollResponse(int Id, string EmployeeName, string JobTitle, bool IsUnhealthy, 
    bool IsPericulosity, decimal BaseSalary, decimal Gross, float OverTime, decimal Commission,
    decimal Additionals, decimal Deductions, decimal Net, DateTime CreationDate);
