namespace RHWebApplication.Web.Responses;

public class UserInfo
{
    public int Id;
    public string UserName = string.Empty;
    public string Company = string.Empty;
    public string Role { get; set; } = string.Empty;
}
