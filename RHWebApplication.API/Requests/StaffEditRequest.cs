namespace RHWebApplication.API.Requests;
public record StaffEditRequest(int Id, string Login, string Password, string Name, string Email, string CompanyTradeName);

