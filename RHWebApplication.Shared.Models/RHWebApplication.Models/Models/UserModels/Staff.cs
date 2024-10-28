using RHWebApplication.Shared.Models.CompanyModels;

namespace RHWebApplication.Shared.Models.UserModels;
public class Staff : User
{
	public Staff() { }

	public Staff(string login, string password, string name, string email, Company company)
	{
		UserType = "Staff";
        Company = company;
        CompanyId = company.Id;
        CompanyName = Company.CorporateName;
    }
}
