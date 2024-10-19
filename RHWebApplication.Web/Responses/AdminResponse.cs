namespace RHWebApplication.Web.Responses;
public record AdminResponse(int Id, string Name, string Login, string Email,
                    DateTime CreationDate, bool IsActive);