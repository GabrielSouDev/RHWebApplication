using RHWebApplication.Shared.Models.UserModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RHWebApplication.Shared.Models.JobModels;
public class Job
{
    #pragma warning disable CS8618
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
    public virtual ICollection<Employee> Employees { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsUnhealthy { get; set; } = false; //insalubre
    public bool IsPericulosity { get; set; } = false; //periculosidade
    public decimal BaseSalary { get; set; } = default;

}
