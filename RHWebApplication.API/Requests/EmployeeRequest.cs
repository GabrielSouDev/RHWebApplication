﻿namespace RHWebApplication.API.Requests;

public record EmployeeRequest(string Login, string Password, string Name, string Email, string  JobTitle, int Dependents, string CompanyTradeName);