﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RHWebApplication.Shared.Models.PayrollModels;
using RHWebApplication.Shared.Models.JobModels;

namespace RHWebApplication.Shared.Models.UserModels;
public class Employee : User
{
    //implementar DataAnottation e saida de erros.
#pragma warning disable CS8618
    public Employee() {}
    public Employee(string login, string password, string name, string email, Job job, int dependents)
    : base(login, password, name, email)
    {
        PayrollHistory = new List<Payroll>();
        Job = job;
		Dependents = dependents;
	}
    public int Dependents { get; set; } = 0;
    public virtual ICollection<Payroll> PayrollHistory { get; set; }
    public int JobId { get; set; }
    public virtual Job Job { get; set; }
    public DateTime? TerminationDate { get; set; } = null;
}
