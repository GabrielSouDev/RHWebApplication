using Microsoft.AspNetCore.Mvc;
using RHWebApplication.API.Requests;
using RHWebApplication.API.Responses;
using RHWebApplication.Database;
using RHWebApplication.Shared.Models.CompanyModels;

namespace RHWebApplication.API.EndPoints;

public static class CompanyExtensions
{
    public static void AddEndPointsCompanys(this WebApplication app)
    {
        var CompanyGroup = app.MapGroup("/Company").WithTags("Company EndPoints");

		CompanyGroup.MapGet("/", async ([FromServices]DAL<Company> dalCompany) =>
        {
            var companys = await dalCompany.ToListAsync();
            var companysResponse = new List<CompanyResponse>();
			foreach (var company in companys)
            {
				companysResponse.Add(new CompanyResponse(company.Id, company.TradeName, company.CorporateName, company.CNPJ));
            }

            return Results.Ok(companysResponse);
        });

        CompanyGroup.MapGet("/Names", async ([FromServices] DAL<Company> dalCompany) =>
        {
            var companys = await dalCompany.ToListAsync();
            List<string> CompanyList = new List<string>();
            foreach (var company in companys)
            {
                if (company.CorporateName is not null)
                    CompanyList.Add(company.CorporateName);
            }

            return Results.Ok(CompanyList);
        });

        CompanyGroup.MapGet("/{Id}", async ([FromServices]DAL<Company> dalCompany, int Id) =>
        {
            var company = await dalCompany.FindByAsync(a => a.Id == Id);
            if(company is null)
                return Results.NotFound();

            return Results.Ok(new CompanyResponse(company.Id, company.TradeName, company.CorporateName, company.CNPJ));
        });

		CompanyGroup.MapPost("/", async ([FromServices]DAL<Company> dalCompany, [FromBody]CompanyRequest companyRequest) =>
        {
            
            var company = await dalCompany.FindByAsync( c => c.CNPJ == companyRequest.CNPJ);

            if(company is null)
            { 
                await dalCompany.AddAsync(new Company(companyRequest.TradeName, companyRequest.CorporateName, companyRequest.CNPJ));
                return Results.Created();
            }
            return Results.Conflict("Company is already created!");
        });

		CompanyGroup.MapPut("/", async ([FromServices]DAL<Company> dalCompany, [FromBody]CompanyEditRequest companyEditRequest) => 
        {
            var company = await dalCompany.FindByAsync(a=> a.Id == companyEditRequest.Id);
            if (company is null)
                return Results.NotFound();

            company.TradeName = companyEditRequest.TradeName;
            company.CorporateName = companyEditRequest.CorporateName;
            company.CNPJ = companyEditRequest.CNPJ;

            await dalCompany.UpdateAsync(company);
            return Results.NoContent();
        });

		CompanyGroup.MapDelete("/{Id}", async ([FromServices]DAL<Company> dalCompany, int Id) =>
        {
            var company = await dalCompany.FindByAsync(a=>a.Id == Id);
            if(company is null)
                return Results.NotFound();

            await dalCompany.DeleteAsync(company);
            return Results.NoContent();
        });
    }
}

