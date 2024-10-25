namespace RHWebApplication.API.Responses;
public record PayrollResponse(int Id, string EmployeeName, string JobTitle, int UnhealthyLevel, bool IsPericulosity,
	float OverTime, decimal OverTimeAditionals, decimal PericulosityValue, decimal UnhealthyValue,
	decimal Commission, decimal INSSDeduction, decimal IRRFDeduction, decimal Deductions, decimal baseSalary,
	decimal Net, decimal Gross, DateTime CreationDate);

