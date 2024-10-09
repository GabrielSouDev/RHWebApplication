using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using RH_WebApplication.API.Requests;
using RHWebApplication.Database;
using RHWebApplication.Shared.Models.JobModels;

namespace RH_WebApplication.API.EndPoints;

public static class JobsExtensions
{
    public static void AddEndPointsJob(this WebApplication app)
    {
        app.MapGet("/Job", async ([FromServices]DAL<Job> dalJobs) =>
        {
            return Results.Ok(await dalJobs.ListAsync());
        });

        app.MapGet("/Job/{Id}", async ([FromServices]DAL<Job> dalJobs, int Id) =>
        {
            var Job = await dalJobs.FindByAsync(a => a.Id.Equals(Id));
            if(Job is null)
                return Results.NotFound();

            return Results.Ok(Job);
        });

        app.MapPost("/job", async ([FromServices]DAL<Job> dalJobs, [FromBody]JobRequest jobRequest) =>
        {
            await dalJobs.AddAsync(new Job(jobRequest.Title, jobRequest.Description, jobRequest.IsUnhealthy,
                jobRequest.IsPericulosity,jobRequest.BaseSalary));
            return Results.Created();
        });

        app.MapPut("/Job", async ([FromServices]DAL<Job> dalJobs, [FromBody]JobEditRequest jobEditRequest) => 
        {
            var job = await dalJobs.FindByAsync(a=> a.Id.Equals(jobEditRequest.Id));
            if (job is null)
                return Results.NotFound();

            job.Title = jobEditRequest.Title;
            job.Description = jobEditRequest.Description;
            job.IsUnhealthy = jobEditRequest.IsUnhealthy;
            job.IsPericulosity = jobEditRequest.IsPericulosity;
            job.BaseSalary = jobEditRequest.BaseSalary;
            await dalJobs.UpdateAsync(job);
            return Results.NoContent();
        });

        app.MapDelete("/Job/{Id}", async ([FromServices]DAL<Job> dalJobs, int Id) =>
        {
            var job = await dalJobs.FindByAsync(a=>a.Id.Equals(Id));
            if(job is null)
                return Results.NotFound();

            await dalJobs.DeleteAsync(job);
            return Results.NoContent();
        });
    }
}

