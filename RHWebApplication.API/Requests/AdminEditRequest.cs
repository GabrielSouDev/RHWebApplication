﻿namespace RHWebApplication.API.Requests;
public record AdminEditRequest(int Id, string Login, string Password, string Name, string Email, string CompanyTradeName);

