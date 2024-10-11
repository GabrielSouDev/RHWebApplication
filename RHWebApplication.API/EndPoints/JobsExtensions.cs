using Microsoft.AspNetCore.Mvc;
using RHWebApplication.API.Requests;
using RHWebApplication.API.Responses;
using RHWebApplication.Database;
using RHWebApplication.Shared.Models.JobModels;

namespace RHWebApplication.API.EndPoints;

public static class JobsExtensions
{
    public static void AddEndPointsJob(this WebApplication app)
    {
        var jobGroup = app.MapGroup("/Job").WithTags("Job EndPoints");

        jobGroup.MapGet("/", async ([FromServices]DAL<Job> dalJobs) =>
        {
            var jobs = await dalJobs.ToListAsync();
            var jobsResponse = new List<JobResponse>();
            foreach (var job in jobs)
            {
                jobsResponse.Add(new JobResponse(job.Id, job.Title, job.Description, job.IsUnhealthy, job.IsPericulosity, job.BaseSalary));
            }
            return Results.Ok(jobsResponse);
        });

        jobGroup.MapGet("/{Id}", async ([FromServices]DAL<Job> dalJobs, int Id) =>
        {
            var job = await dalJobs.FindByAsync(a => a.Id == Id);
            if(job is null)
                return Results.NotFound();

            return Results.Ok(new JobResponse(job.Id, job.Title, job.Description, job.IsUnhealthy, job.IsPericulosity, job.BaseSalary));
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

