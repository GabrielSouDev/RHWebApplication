namespace RHWebApplication.Web.Requests;

public record PayrollRequest(string EmployeeName, float OverTime, decimal Commission);