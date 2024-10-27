using Microsoft.AspNetCore.Mvc;
using RHWebApplication.API.Requests;
using RHWebApplication.API.Responses;
using RHWebApplication.Database;
using RHWebApplication.Shared.Models.CompanyModels;

namespace RHWebApplication.API.EndPoints;

public static class JobsExtensions
{
    public static void AddEndPointsJobTitles(this WebApplication app)
    {
        var jobGroup = app.MapGroup("/Job").WithTags("Job EndPoints");

        jobGroup.MapGet("/", async ([FromServices]DAL<JobTitle> dalJobs) =>
        {
            var jobs = await dalJobs.ToListAsync();
            var jobsResponse = new List<JobResponse>();
            foreach (var job in jobs)
            {
                jobsResponse.Add(new JobResponse(job.Id, job.Name, job.Description, job.UnhealthyLevel, job.IsPericulosity, job.OverTimeValue, job.BaseSalary, job.Company.CorporateName));
            }

            return Results.Ok(jobsResponse);
        });


        jobGroup.MapGet("/{company}", async ([FromServices] DAL<JobTitle> dalJobs, string company) =>
        {
            var jobs = await dalJobs.ToListAsync();
            var jobsResponse = new List<JobResponse>();
            foreach (var job in jobs)
            {
                if (job.Company.CorporateName == company || company == string.Empty)
                {
                    jobsResponse.Add(new JobResponse(job.Id, job.Name, job.Description, job.UnhealthyLevel, job.IsPericulosity, job.OverTimeValue, job.BaseSalary, job.Company.CorporateName));
                }
            }

            return Results.Ok(jobsResponse);
        });

        jobGroup.MapGet("/Titles/{company}", async ([FromServices] DAL<JobTitle> dalJobs, string company) =>
        {
            var jobs = await dalJobs.ToListAsync();
            List<string> titleList = new List<string>();
            foreach (var job in jobs)
            {
                if(job.Name is not null)
                    if(job.Company.CorporateName == company || company == string.Empty)
                        titleList.Add(job.Name);
            }

            return Results.Ok(titleList);
        });

        jobGroup.MapGet("/{id:int}", async ([FromServices]DAL<JobTitle> dalJobs, int Id) =>
        {
            var job = await dalJobs.FindByAsync(a => a.Id == Id);
            if(job is null)
                return Results.NotFound();

            return Results.Ok(new JobResponse(job.Id, job.Name, job.Description, job.UnhealthyLevel, job.IsPericulosity, job.OverTimeValue, job.BaseSalary, job.Company.CorporateName));
        });

        jobGroup.MapPost("/", async ([FromServices]DAL<JobTitle> dalJobs, [FromServices]DAL<Company> dalCompanys, [FromBody]JobRequest jobRequest) =>
        {
            
            var job = await dalJobs.FindByAsync( j => j.Name == jobRequest.Title);

            if(job is null)
            {
                var company = await dalCompanys.FindByAsync(c => c.CorporateName == jobRequest.CompanyName);
                if(company is not null)
                {
				    await dalJobs.AddAsync(new JobTitle(jobRequest.Title, jobRequest.Description, jobRequest.UnhealthyLevel,
                        jobRequest.IsPericulosity, jobRequest.OverTimeValue, jobRequest.BaseSalary, company));
                    return Results.Created();
                }
                else
                    return Results.NotFound("Company Name is not found!");
            }
            return Results.Conflict("Job title is already created!");
        });

        jobGroup.MapPut("/", async ([FromServices]DAL<JobTitle> dalJobs, [FromBody]JobEditRequest jobEditRequest) => 
        {
            var job = await dalJobs.FindByAsync(a=> a.Id == jobEditRequest.Id);
            if (job is null)
                return Results.NotFound();

            job.Name = jobEditRequest.Title;
            job.Description = jobEditRequest.Description;
            job.UnhealthyLevel = jobEditRequest.UnhealthyLevel;
            job.IsPericulosity = jobEditRequest.IsPericulosity;
            job.OverTimeValue = jobEditRequest.OverTimeValue;
            job.BaseSalary = jobEditRequest.BaseSalary;
            await dalJobs.UpdateAsync(job);
            return Results.NoContent();
        });

        jobGroup.MapDelete("/{Id}", async ([FromServices]DAL<JobTitle> dalJobs, int Id) =>
        {
            var job = await dalJobs.FindByAsync(a=>a.Id == Id);
            if(job is null)
                return Results.NotFound();

            await dalJobs.DeleteAsync(job);
            return Results.NoContent();
        });
    }
}

