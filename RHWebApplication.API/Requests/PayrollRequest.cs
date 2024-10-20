namespace RHWebApplication.API.Requests;

public record PayrollRequest(int Id, string EmployeeName, float OverTime, decimal Commission);