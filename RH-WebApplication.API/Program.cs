using Microsoft.EntityFrameworkCore;
using RH_WebApplication.API.EndPoints;
using RHWebApplication.Database;
using RHWebApplication.Shared.Models.JobModels;
using RHWebApplication.Shared.Models.UserModels;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ApplicationContext>((options) =>
{
    options.UseSqlServer(builder.Configuration["ConnectionStrings:RHDatabase"])
    .UseLazyLoadingProxies();
});

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<DAL<User>>();
builder.Services.AddTransient<DAL<Employee>>();
builder.Services.AddTransient<DAL<Job>>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.AddEndPointsUsers();
app.AddEndPointsEmployees();
app.AddEndPointsJob();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
