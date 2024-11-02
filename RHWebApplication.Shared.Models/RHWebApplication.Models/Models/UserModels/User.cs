using RHWebApplication.Shared.Models.CompanyModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace RHWebApplication.Shared.Models.UserModels;
public class User
{
    public User() { }
    public User(string login, string password, string name, string email, int companyId) 
    {
        Login = login;
        Password = password;
        Name = name;
        Email = email;
        CompanyId = companyId;
        CreationDate = DateTime.UtcNow;
        IsActive = true;
    }
    public int Id { get; init; }
    [ForeignKey("Companies")]
    public int CompanyId { get; set; }
    public virtual Company Company { get; set; } 
    public string Login { get; set; }
    public string Password { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string UserType { get; set; }
    public DateTime CreationDate { get; init; }
    public bool IsActive { get; set; }
}
