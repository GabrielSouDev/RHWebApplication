using RHWebApplication.Shared.Models.CompanyModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.Design;

namespace RHWebApplication.Shared.Models.UserModels;
public class Admin : User
{
    public Admin() { }

    public Admin(string login, string password, string name, string email, int companyId) : base(login, password, name, email, companyId)
    {
        UserType = "Admin";
    }
}
