namespace RHWebApplication.Web.Requests;

public class JobRequest
{
    public JobRequest() { }
    public JobRequest(int id, string title, string description, int unhealthyLevel, bool isPericulosity, decimal overTimeValue, decimal baseSalary) 
    {
        Id = id;
        Title = title;
        Description = description;
        UnhealthyLevel = unhealthyLevel;
        IsPericulosity = isPericulosity;
        OverTimeValue = overTimeValue;
        BaseSalary = baseSalary;
    }
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int UnhealthyLevel { get; set; } = 0;
    public bool IsPericulosity { get; set; } = false;
    public decimal OverTimeValue { get; set; } = 0;
    public decimal BaseSalary { get; set; } = default;
}

