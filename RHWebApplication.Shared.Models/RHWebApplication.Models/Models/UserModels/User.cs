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
        CompanyName = Company.CorporateName;
        CreationDate = DateTime.UtcNow;
        IsActive = true;
    }
    public int Id { get; init; }
    [ForeignKey("Companies")]
    public int CompanyId { get; set; }
    public virtual Company Company { get; set; } = new Company();
    public string Login { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string UserType { get; set; } = string.Empty;
    public string CompanyName {get; set;} = string.Empty;
    public DateTime CreationDate { get; init; } = default;
    public bool IsActive { get; set; } = true;
}
