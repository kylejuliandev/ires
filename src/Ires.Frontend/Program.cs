using Ires.Data;
using Ires.Frontend;
using Ires.Frontend.Components;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var dbLocation = builder.Configuration["DB_LOCATION"]
    ?? throw new InvalidOperationException("DB_LOCATION configuration is missing");

builder.AddServiceDefaults();

// Add services to the container.

builder.Services.AddAuthentication("cookie")
    .AddCookie("cookie", options =>
    {
        options.Cookie.Name = "ires_auth_cookie";
        options.LoginPath = "/signin";
    });
builder.Services.AddAuthorization();
builder.Services.AddCascadingAuthenticationState();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddHttpClient();

builder.Services.AddDbContext<IresDbContext>(
    db => db.UseSqlite($"Data Source={dbLocation}"));

builder.Services.AddQuickGridEntityFrameworkAdapter();

builder.Services.AddHttpContextAccessor();

// Migrations
builder.Services.AddHostedService<Worker>();

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    string[] supportedCultures = ["en-GB"];
    options.SetDefaultCulture(supportedCultures[0])
        .AddSupportedCultures(supportedCultures)
        .AddSupportedUICultures(supportedCultures);
});

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
