using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RHWebApplication.API.EndPoints;
using RHWebApplication.Database;
using RHWebApplication.Shared.Models.JobModels;
using RHWebApplication.Shared.Models.PayrollModels;
using RHWebApplication.Shared.Models.UserModels;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddCors(options =>
{
options.AddPolicy("AllowAll",
    builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "https://localhost:7019",
            ValidAudience = "https://localhost:7029",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("its-my-secret-key"))
        };
    });

builder.Services.AddAuthorization();


builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazor", 
        builder => builder.WithOrigins("https://localhost:7184")
        .AllowAnyMethod()
        .AllowAnyHeader());
});

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<DAL<User>>();
builder.Services.AddTransient<DAL<Admin>>();
builder.Services.AddTransient<DAL<Employee>>();
builder.Services.AddTransient<DAL<Payroll>>();
builder.Services.AddTransient<DAL<Job>>();

builder.Services.AddDbContext<ApplicationContext>((options) =>
{
    options.UseSqlServer(builder.Configuration["ConnectionStrings:RHDatabase"])
    .UseLazyLoadingProxies();
});

var app = builder.Build();
app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowBlazor");
app.UseHttpsRedirection();
app.UseAuthorization();

app.AddEndPointsUsers();
app.AddEndPointsAdmins();
app.AddEndPointsEmployees();
app.AddEndPointsPayrolls();
app.AddEndPointsJob();

app.MapControllers();

app.Run();