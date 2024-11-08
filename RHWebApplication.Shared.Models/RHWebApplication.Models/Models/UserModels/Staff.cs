using RHWebApplication.Shared.Models.CompanyModels;

namespace RHWebApplication.Shared.Models.UserModels;
public class Staff : User
{
    public Staff() { }
    public Staff(string login, string password, string name, string email, int companyId) : base(login, password, name, email, companyId)
	{
        Login = login;
        Password = password;
        Email = email;
        UserType = "Staff";
        CompanyId = companyId;
    }
}
