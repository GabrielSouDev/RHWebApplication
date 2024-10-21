var builder = DistributedApplication.CreateBuilder(args);

var API = builder.AddProject<Projects.RHWebApplication_API>("rhwebapplication-api");
builder.AddProject<Projects.RHWebApplication_Web>("rhwebapplication-web").WithReference(API);

builder.Build().Run();
