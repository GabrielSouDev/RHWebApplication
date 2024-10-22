namespace RHWebApplication.Web.Requests;
public class LoginRequest
{
    public LoginRequest() {}
    public LoginRequest(string login, string password)
    {
        Login = login;
        Password = Password;
    }
    public string Login { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
