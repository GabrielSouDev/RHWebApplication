namespace RHWebApplication.API.Requests;

public record PayrollEditRequest(int Id, float OverTime, decimal Commission);