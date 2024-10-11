using Microsoft.EntityFrameworkCore;
using RHWebApplication.API.EndPoints;
using RHWebApplication.Database;
using RHWebApplication.Shared.Models.JobModels;
using RHWebApplication.Shared.Models.PayrollModels;
using RHWebApplication.Shared.Models.UserModels;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ApplicationContext>((options) =>
{
    options.UseSqlServer(builder.Configuration["ConnectionStrings:RHDatabase"])
    .UseLazyLoadingProxies();
});

builder.Services.AddControllers();

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<DAL<User>>();
builder.Services.AddTransient<DAL<Admin>>();
builder.Services.AddTransient<DAL<Employee>>();
builder.Services.AddTransient<DAL<Payroll>>();
builder.Services.AddTransient<DAL<Job>>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.AddEndPointsUsers();
app.AddEndPointsAdmins();
app.AddEndPointsEmployees();
app.AddEndPointsPayrolls();
app.AddEndPointsJob();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();