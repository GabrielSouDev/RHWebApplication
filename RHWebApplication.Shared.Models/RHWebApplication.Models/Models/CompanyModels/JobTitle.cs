using RHWebApplication.Shared.Models.UserModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RHWebApplication.Shared.Models.CompanyModels;
public class JobTitle
{
#pragma warning disable CS8618
    public JobTitle() { }
    public JobTitle(string name, string description, int unhealthyLevel, bool isPericulosity, decimal overTimeValue, decimal baseSalary, Company company)
    {
        Name = name;
        Description = description;
        UnhealthyLevel = unhealthyLevel;
        IsPericulosity = isPericulosity;
        OverTimeValue = overTimeValue;
        BaseSalary = baseSalary;
        Company = company;
	}
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; init; }
    public virtual ICollection<Employee> Employees { get; set; }
    public virtual int CompanyID { get; set; }
    public virtual Company Company { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int UnhealthyLevel { get; set; }
    public bool IsPericulosity { get; set; }
    public decimal OverTimeValue { get; set; }
    public decimal BaseSalary { get; set; }

}
/*
auxílio-transporte, vale-alimentação e plano de saúde;
 */
