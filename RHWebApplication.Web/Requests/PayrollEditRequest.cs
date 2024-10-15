namespace RHWebApplication.Web.Requests;

public record PayrollEditRequest(int Id, float OverTime, decimal Commission);