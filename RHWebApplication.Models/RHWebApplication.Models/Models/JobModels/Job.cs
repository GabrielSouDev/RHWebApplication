using RHWebApplication.Shared.Models.UserModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RHWebApplication.Shared.Models.JobModels;
public class Job
{
#pragma warning disable CS8618
    public Job() { }
    public Job(string title, string description, int unhealthyLevel, bool isPericulosity, decimal OverTimeValue, decimal baseSalary)
    {
        Title = title;
        Description = description;
        UnhealthyLevel = unhealthyLevel;
        IsPericulosity = isPericulosity;
        BaseSalary = baseSalary;
    }
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; init; }
    public virtual ICollection<Employee> Employees { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int UnhealthyLevel { get; set; } = 0;
    public bool IsPericulosity { get; set; } = false;
    public decimal OverTimeValue { get; set; } = 0;
    public decimal BaseSalary { get; set; } = default;

}
/*
auxílio-transporte, vale-alimentação e plano de saúde;
 */
