using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RHWebApplication.Shared.Models.PayrollModels;
using RHWebApplication.Shared.Models.CompanyModels;

namespace RHWebApplication.Shared.Models.UserModels;
public class Employee : User
{
    //implementar DataAnottation e saida de erros.
#pragma warning disable CS8618
    public Employee() {}
    public Employee(string login, string password, string name, string email, int companyId, JobTitle job, int dependents) : base(login, password, name, email, companyId)
    {
        PayrollHistory = new List<Payroll>();
        Job = job;
		Dependents = dependents;
        UserType = "Employee";
    }
    public int Dependents { get; set; }
    public virtual ICollection<Payroll> PayrollHistory { get; set; }
    public int JobId { get; set; }
    public virtual JobTitle Job { get; set; }
    public DateTime? TerminationDate { get; set; } = null;
}


