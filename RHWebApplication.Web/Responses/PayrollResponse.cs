using RHWebApplication.Shared.Models.UserModels;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using RHWebApplication.Shared.Models.CompanyModels;
using RHWebApplication.Shared.Models.PayrollModels;

namespace RHWebApplication.Web.Responses;
public class PayrollResponse
{
	public PayrollResponse() { }
	public PayrollResponse(int id, int employeeId, string employeeName, string companyTradeName, int companyCNPJ, string jobTitle, int unhealthyLevel, bool isPericulosity,
	float overTime, decimal overTimeAditionals, decimal periculosityValue, decimal unhealthyValue,
	decimal commission, decimal inssDeduction, decimal irrfDeduction, decimal deductions, decimal baseSalary,
	decimal net, decimal gross, DateTime creationDate)
	{
		Id = id;
		EmployeeId = employeeId;
		EmployeeName = employeeName;
		CompanyTradeName = companyTradeName;
		CompanyCNPJ = companyCNPJ;
		JobTitle = jobTitle;
		UnhealthyLevel = unhealthyLevel;
		IsPericulosity = isPericulosity;
		OverTime = overTime;
		OverTimeAditionals = overTimeAditionals;
		PericulosityValue = periculosityValue;
		UnhealthyValue = unhealthyValue;
		Commission = commission;
		INSSDeduction = inssDeduction;
		IRRFDeduction = irrfDeduction;
		Deductions = deductions;
		BaseSalary = baseSalary;
		Gross = gross;
		Net = net;
		CreationDate = creationDate;

	}
	public int Id { get; set; }
	public int EmployeeId { get; set; }
	public string EmployeeName { get; set; }
	public string CompanyTradeName { get; set; }
	public int CompanyCNPJ { get; set; }
	public string JobTitle { get; set; }
	public bool IsPericulosity { get; set; }
	public decimal PericulosityValue { get; set; }
	public float OverTime { get; set; }
	public decimal OverTimeAditionals { get; set; }
	public int UnhealthyLevel { get; set; }
	public decimal UnhealthyValue { get; set; }
	public decimal Commission { get; set; }
	public decimal INSSDeduction { get; set; }
	public decimal IRRFDeduction { get; set; }
	public decimal Deductions { get; set; }
	public decimal BaseSalary { get; set; }
	public decimal Net { get; set; } // Salario Liquido
	public decimal Gross { get; set; }
	public DateTime CreationDate { get; init; }
}