using RHWebApplication.Shared.Models.CompanyModels;

namespace RHWebApplication.Shared.Models.UserModels;
public class Staff : User
{
	public Staff() { }

	public Staff(string login, string password, string name, string email, Company company)
	{
		UserType = "Staff";
        CompanyId = company.Id;
        Company = company;
        CompanyName = company.CorporateName;
    }
}
