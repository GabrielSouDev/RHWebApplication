using System.ComponentModel.DataAnnotations;

public class EmployeeResponse
{
	public EmployeeResponse(int id, string login, string name, string email, int dependents, string jobTitle, DateTime creationDate, DateTime? terminationDate, bool isActive, string companyTradeName)
	{
		Id = id;
		Login = login;
		Name = name;
		Email = email;
		Dependents = dependents;
		JobTitle = jobTitle;
		CreationDate = creationDate;
		TerminationDate = terminationDate;
		IsActive = isActive;
		CompanyTradeName = companyTradeName;
	}
	public int Id { get; set; }
	public string Login { get; set; } = string.Empty;
	public string Name { get; set; } = string.Empty;
	public string Email { get; set; } = string.Empty;
	public int Dependents { get; set; }
	public string JobTitle { get; set; } = string.Empty;
	public DateTime CreationDate { get; init; } = default;
	public DateTime? TerminationDate { get; set; } = default;
	public bool IsActive { get; set; } = true;
	public string CompanyTradeName { get; set; } = string.Empty;
}