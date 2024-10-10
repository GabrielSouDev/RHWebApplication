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
        var jobGroup = app.MapGroup("/Job").WithTags("Job EndPoints");

        jobGroup.MapGet("/", async ([FromServices]DAL<Job> dalJobs) =>
        {
            return Results.Ok(await dalJobs.ToListAsync());
        });

        jobGroup.MapGet("/{Id}", async ([FromServices]DAL<Job> dalJobs, int Id) =>
        {
            var Job = await dalJobs.FindByAsync(a => a.Id == Id);
            if(Job is null)
                return Results.NotFound();

            return Results.Ok(Job);
        });

        jobGroup.MapPost("/", async ([FromServices]DAL<Job> dalJobs, [FromBody]JobRequest jobRequest) =>
        {
            await dalJobs.AddAsync(new Job(jobRequest.Title, jobRequest.Description, jobRequest.IsUnhealthy,
                jobRequest.IsPericulosity,jobRequest.BaseSalary));
            return Results.Created();
        });

        jobGroup.MapPut("/", async ([FromServices]DAL<Job> dalJobs, [FromBody]JobEditRequest jobEditRequest) => 
        {
            var job = await dalJobs.FindByAsync(a=> a.Id == jobEditRequest.Id);
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

        jobGroup.MapDelete("/{Id}", async ([FromServices]DAL<Job> dalJobs, int Id) =>
        {
            var job = await dalJobs.FindByAsync(a=>a.Id == Id);
            if(job is null)
                return Results.NotFound();

            await dalJobs.DeleteAsync(job);
            return Results.NoContent();
        });
    }
}

