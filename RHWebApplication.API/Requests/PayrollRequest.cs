namespace RHWebApplication.API.Requests;

public record PayrollRequest(string EmployeeName, float OverTime, decimal Commission);