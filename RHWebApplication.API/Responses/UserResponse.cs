using System.ComponentModel.DataAnnotations;

namespace RHWebApplication.API.Responses;

public class UserResponse
{
    public UserResponse(int id, string name,string login, string email, DateTime creationDate, bool isActive)
    {
        Id = id;
        Name = name;
        Login = login;
        Email = email;
        CreationDate = creationDate;
        IsActive = isActive;
    }
    public int Id { get; set; }
    public string Login { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    public DateTime CreationDate { get; init; } = default;
    public bool IsActive { get; set; } = true;
}