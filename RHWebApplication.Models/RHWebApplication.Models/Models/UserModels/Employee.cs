using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RHWebApplication.Shared.Models.PayrollModels;
using RHWebApplication.Shared.Models.JobModels;

namespace RHWebApplication.Shared.Models.UserModels;
public class Employee : User
{
    //implementar DataAnottation e saida de erros.
    public Employee() {}
    public Employee(string login, string password, string name, string email, Job job)
    : base(login, password, name, email)
    {
        PayrollHistory = new List<Payroll>();
        Job = job;
    }
    public virtual ICollection<Payroll>? PayrollHistory { get; set; }
    public virtual Job? Job { get; set; } = null;
    public DateTime TerminationDate { get; set; } = default;
}
