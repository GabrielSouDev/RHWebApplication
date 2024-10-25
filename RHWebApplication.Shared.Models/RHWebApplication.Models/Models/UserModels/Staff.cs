namespace RHWebApplication.Shared.Models.UserModels;
public class Staff : User
{
	public Staff() { }

	public Staff(string login, string password, string name, string email)
	: base(login, password, name, email)
	{
		UserType = "Staff";
	}
}
