using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RHWebApplication.Shared.Models.UserModels;
public class User
{
    public User() {}
    public User(string login, string password, string name, string email)
    {
        Login = login;
        Password = password;
        Name = name;
        Email = email;
        CreationDate = DateTime.Now;
    }
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; init; }
    public string Login { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime CreationDate { get; init; } = default;
}
