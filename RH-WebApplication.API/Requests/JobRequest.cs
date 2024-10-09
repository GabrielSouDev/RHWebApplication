namespace RH_WebApplication.API.Requests;

public record JobRequest(string title, string description, bool unhealthy, bool periculosity, decimal baseSalary);


