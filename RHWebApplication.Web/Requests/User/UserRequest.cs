using System.ComponentModel.DataAnnotations;

namespace RHWebApplication.Web.Requests;

public class UserRequest
{ 
	public UserRequest() { }

	public UserRequest(int id, string login, string password, string name, string email)
	{
		Id = id;
		Login = login;
		Password = password;
		Name = name;
		Email = email;

	}
	public int Id { get; set; }
	public string Login { get; set; } = string.Empty;
	public string Password { get; set; } = string.Empty;
	public string Name { get; set; } = string.Empty;
	[EmailAddress]
	public string Email { get; set; } = string.Empty;
}