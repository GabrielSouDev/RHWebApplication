using RHWebApplication.Shared.Models.JobModels;

namespace RHWebApplication.Web.Responses;
public class JobResponse
{
    public JobResponse(int id, string title, string description, int unhealthyLevel, bool isPericulosity, decimal overTimeValue, decimal baseSalary)
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
    public string Title { get; set; }
    public string Description { get; set; }
    public int UnhealthyLevel { get; set; }
    public bool IsPericulosity { get; set; }
    public decimal OverTimeValue { get; set; }
    public decimal BaseSalary { get; set; }
}