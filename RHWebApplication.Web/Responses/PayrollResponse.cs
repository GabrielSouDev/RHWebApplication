using RHWebApplication.Shared.Models.UserModels;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using RHWebApplication.Shared.Models.CompanyModels;
using RHWebApplication.Shared.Models.PayrollModels;

namespace RHWebApplication.Web.Responses;

public record PayrollResponse(int Id, string EmployeeName, string JobTitle, int UnhealthyLevel, bool IsPericulosity, 
	float OverTime, decimal OverTimeAditionals, decimal PericulosityValue,  decimal UnhealthyValue,
	decimal Commission, decimal INSSDeduction, decimal IRRFDeduction, decimal Deductions, decimal BaseSalary,
	decimal Net, decimal Gross, DateTime CreationDate);




