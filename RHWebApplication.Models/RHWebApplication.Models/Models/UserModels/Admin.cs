using RHWebApplication.Shared.Models.JobModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RHWebApplication.Shared.Models.UserModels;
public class Admin : User
{
    public Admin() {}

    public Admin(string login, string password, string name, string email)
    : base(login, password, name, email)
    {
    }
}
