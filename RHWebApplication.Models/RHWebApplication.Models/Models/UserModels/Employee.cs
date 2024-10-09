using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RHWebApplication.Shared.Models.PayrollModels;
using RHWebApplication.Shared.Models.JobModels;

namespace RHWebApplication.Shared.Models.UserModels;
public class Employee : User
{
    public Employee() {}
    public Employee(Job job)
    {
        PayrollHistory = new List<Payroll>();
        Job = job;

    }
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public virtual ICollection<Payroll> PayrollHistory { get; private set; }
    public virtual Job Job { get; set; }
    public DateTime TerminationDate { get; set; } = default;
}
