using RHWebApplication.Shared.Models.UserModels;

namespace RHWebApplication.API.Requests;

public record PayrollRequest(int EmployeeId, float OverTime, decimal Commission);