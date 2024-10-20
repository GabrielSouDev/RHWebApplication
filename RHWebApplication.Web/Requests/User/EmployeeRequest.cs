using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;

namespace RHWebApplication.Web.Responses;
public class EmployeeRequest
{
    public EmployeeRequest() { }
    public EmployeeRequest(int id, string login, string password, string name, string email, int dependents, string jobTitle, DateTime creationDate, DateTime terminationDate, bool isActive)
    {
        Id = id;
        Login = login;
        Password = password;
        Name = name;
        Email = email;
        Dependents = dependents;
		JobTitle = jobTitle;
        CreationDate = creationDate;
        TerminationDate = terminationDate;
        IsActive = isActive;
    }
    public int Id { get; set; }
    public string Login { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    public int Dependents { get; set; }
    public string JobTitle { get; set; } = string.Empty;
    public DateTime CreationDate { get; init; } = default;
    public DateTime? TerminationDate { get; set; } = default;
    public bool IsActive { get; set; } = true;
}