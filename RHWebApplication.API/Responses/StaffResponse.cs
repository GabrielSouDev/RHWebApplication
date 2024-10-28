namespace RHWebApplication.API.Responses;
public record StaffResponse(int Id, string Name, string Login, string Email,
                    DateTime CreationDate, bool IsActive);