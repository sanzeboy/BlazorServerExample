using Blazored.LocalStorage;
using Blazored.Modal;
using Microsoft.AspNetCore.Authentication.Cookies;
using PopulationPortal.Application;
using PopulationPortal.Application.Infrastructures;
using PopulationPortal.Application.Services.AppUsers;
using PopulationPortal.Infrastructure;
using PopulationPortal.UI;
using PopulationPortal.UI.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<ICurrentUserService, CurrentUserService>();

// Add httpContext
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddHttpContextAccessor();

// Add Application Dependencies
builder.Services.AddApplication();


// Add Infastructure Dependencies
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddBlazoredModal();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(x =>
        {
            x.LoginPath = "/login";
        });
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddBlazoredLocalStorage();

var app = builder.Build();
await app.MigrateDatabase();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();



app.MapStaticAssets();
app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
