using RHWebApplication.Shared.Models.UserModels;

namespace RH_WebApplication.API.Requests;

public record PayrollRequest(string EmployeeName, decimal OverTime, decimal Commission);