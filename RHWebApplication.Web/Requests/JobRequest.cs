namespace RHWebApplication.Web.Requests;

public class JobRequest
{
    public JobRequest() { }

    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsUnhealthy { get; set; } = false;
    public bool IsPericulosity { get; set; } = false;
    public decimal BaseSalary { get; set; } = default;
}

