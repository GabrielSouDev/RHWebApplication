using RHWebApplication.Shared.Models.CompanyModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RHWebApplication.Shared.Models.UserModels;
public class Admin : User
{
    public Admin() { }

    public Admin(string login, string password, string name, string email, Company company)
    {
        Company = company;
        UserType = "Admin";
        Company = company;
        CompanyName = Company.CorporateName;
        CompanyId = Company.Id;
    }
}
