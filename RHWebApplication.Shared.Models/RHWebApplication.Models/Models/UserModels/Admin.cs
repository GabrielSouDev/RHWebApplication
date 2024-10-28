using RHWebApplication.Shared.Models.CompanyModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RHWebApplication.Shared.Models.UserModels;
public class Admin : User
{
    public Admin() { }

    public Admin(string login, string password, string name, string email, Company company)
    {
        UserType = "Admin";
        CompanyId = company.Id;
        Company = company;
        CompanyName = company.CorporateName;
    }
}
