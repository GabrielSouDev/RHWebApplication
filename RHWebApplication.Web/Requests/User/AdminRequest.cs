namespace RHWebApplication.Web.Requests;
public class AdminRequest
{
    public AdminRequest(string login, string password, string name, string email, string companyTradeName)
    {
        Login = login;
        Password = password;
        Name = name;
        Email = email;
        CompanyTradeName = companyTradeName;
    }
    public string Login { get; set; }
    public string Password { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string CompanyTradeName { get; set; }
}
