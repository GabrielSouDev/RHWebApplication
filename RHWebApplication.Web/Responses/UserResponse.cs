namespace RHWebApplication.API.Responses;

public record UserResponse(int Id, string Name, string Login, string Email, DateTime CreationDate, bool IsActive);