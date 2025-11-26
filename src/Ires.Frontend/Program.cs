using Ires.Data;
using Ires.Frontend;
using Ires.Frontend.Components;
using Ires.Frontend.Services;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var dbLocation = builder.Configuration["DB_LOCATION"]
    ?? throw new InvalidOperationException("DB_LOCATION configuration is missing");

builder.AddServiceDefaults();

// Add services to the container.

builder.Services.AddAuthentication("cookie")
    .AddCookie("cookie", options =>
    {
        options.LoginPath = "/signin";
        options.Cookie.Name = "ires_auth_cookie";
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.Cookie.SameSite = SameSiteMode.Strict;
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

builder.Services.AddScoped<BasicAuthenticationService>();

builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo(builder.Configuration["Auth:DataProtectionKeysLocation"]));

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
