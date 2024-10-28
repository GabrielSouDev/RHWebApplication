using RHWebApplication.Shared.Models.CompanyModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RHWebApplication.Shared.Models.UserModels;
public class User
{
    public User() {}
    public int Id { get; init; }
    public int CompanyId { get; set; }
    public virtual Company Company { get; set; }
    public string Login { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string UserType { get; set; } = string.Empty;
    public string CompanyName {get; set;} = string.Empty;
    public DateTime CreationDate { get; init; } = default;
    public bool IsActive { get; set; } = true;
}
