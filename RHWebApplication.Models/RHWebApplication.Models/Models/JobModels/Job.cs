using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RHWebApplication.Shared.Models.JobModels;
public class Job
{
    public Job() {}
    public Job(string title, string description, bool unhealthy, bool periculosity, decimal baseSalary)
    {
        Title = title;
        Description = description;
        Unhealthy = unhealthy;
        Periculosity = periculosity;
        BaseSalary = baseSalary;
    }
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; init; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool Unhealthy { get; private set; } = false;
    public bool Periculosity { get; private set; } = false;
    public decimal BaseSalary { get; set; } = default;

}
