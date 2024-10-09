using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RHWebApplication.Shared.Models.JobModels;
public class Job
{
    public Job() {}
    public Job(string title, string description, bool isUnhealthy, bool isPericulosity, decimal baseSalary)
    {
        Title = title;
        Description = description;
        IsUnhealthy = isUnhealthy;
        IsPericulosity = isPericulosity;
        BaseSalary = baseSalary;
    }
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; init; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsUnhealthy { get; set; } = false; //insalubre
    public bool IsPericulosity { get; set; } = false; //periculosidade
    public decimal BaseSalary { get; set; } = default;

}
