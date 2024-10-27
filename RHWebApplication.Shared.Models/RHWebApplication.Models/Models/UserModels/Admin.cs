using RHWebApplication.Shared.Models.CompanyModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RHWebApplication.Shared.Models.UserModels;
public class Admin : User
{
    public Admin() { }

    public Admin(string login, string password, string name, string email, Company company)
    : base(login, password, name, email)
    {
        Company = company;
        UserType = "Admin";
        CompanyName = company.CorporateName;
    }
    public virtual int CompanyId { get; set; }
	public virtual Company Company { get; set; } = new Company();
}
