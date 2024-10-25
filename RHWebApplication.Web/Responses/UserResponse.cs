using System.ComponentModel.DataAnnotations;

namespace RHWebApplication.Web.Responses;

public class UserResponse
{
	public UserResponse(int id, string login, string name, string email, DateTime creationDate, string userType, bool isActive)
	{
		Id = id;
		Login = login;
		Name = name;
		Email = email;
		CreationDate = creationDate;
		UserType = userType;
        IsActive = isActive;
	}
	public int Id { get; set; }
	public string Login { get; set; } = string.Empty;
	public string Name { get; set; } = string.Empty;
	[EmailAddress]
	public string Email { get; set; } = string.Empty;
	public DateTime CreationDate { get; init; } = default;
	public string UserType { get; set; } = string.Empty;
	public bool IsActive { get; set; } = true;
}