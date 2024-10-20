namespace RHWebApplication.Web.Requests;

public class JobRequest
{
    public JobRequest() { }

    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int UnhealthyLevel { get; set; } = 0;
    public bool IsPericulosity { get; set; } = false;
    public decimal OverTimeValue { get; set; } = 0;
    public decimal BaseSalary { get; set; } = default;
}

