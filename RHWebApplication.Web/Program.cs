using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor;
using MudBlazor.Services;
using RHWebApplication.Web;
using RHWebApplication.Web.Layout;
using RHWebApplication.Web.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMudServices();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();
<<<<<<< HEAD
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7019") });
=======
>>>>>>> 10dc2d2a6f97b77b35e997c3ba77477cbc4998b1
builder.Services.AddScoped<CustomAuthStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<CustomAuthStateProvider>());
builder.Services.AddScoped<AuthenticationState>();

<<<<<<< HEAD
=======
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7019") });
>>>>>>> 10dc2d2a6f97b77b35e997c3ba77477cbc4998b1

await builder.Build().RunAsync();
