using RHWebApplication.Shared.Models.UserModels;

namespace RHWebApplication.Web.Requests;

public class PayrollRequest
{
    public PayrollRequest() { }
    public PayrollRequest(int employeeId, float overTime, decimal commission)
    {
        EmployeeId = employeeId;
        OverTime = overTime;
        Commission = commission;
    }

    public int EmployeeId { get; set; } = 0;
    public float OverTime { get; set; } = 0;
    public decimal Commission { get; set; } = 0;
}
