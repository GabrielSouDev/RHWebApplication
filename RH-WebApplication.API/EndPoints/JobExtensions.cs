using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using RH_WebApplication.API.Requests;
using RHWebApplication.Database;
using RHWebApplication.Shared.Models.JobModels;

namespace RH_WebApplication.API.EndPoints;

public static class JobExtensions
{
    public static void AddEndPointsJob(this WebApplication app)
    {
        app.MapGet("/Job", async ([FromServices]DAL<Job> dal) =>
        {
            return await dal.ListAsync();
        });
        app.MapPost("/job", async ([FromServices]DAL<Job> dal, [FromBody]JobRequest jobRequest) =>
        {
            await dal.AddAsync(new Job(jobRequest.title, jobRequest.description, jobRequest.unhealthy,
                jobRequest.periculosity,jobRequest.baseSalary));
            return;
        });
    }
}

