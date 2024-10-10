using RHWebApplication.Shared.Models.UserModels;

namespace RH_WebApplication.API.Requests;

public record PayrollEditRequest(int Id, decimal OverTime, decimal Commission);