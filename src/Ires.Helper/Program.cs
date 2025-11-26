using Ires.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var dbLocation = builder.Configuration["DB_LOCATION"]
    ?? throw new InvalidOperationException("DB_LOCATION configuration is missing");

builder.AddServiceDefaults();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<IresDbContext>(
    db => db.UseSqlite($"Data Source={dbLocation}"));

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapPost("/register", async ([FromServices] IresDbContext dbContext, [FromBody] CreateUser request) =>
{
    var hasher = new PasswordHasher<string>();
    var result = hasher.HashPassword(request.Username, request.Password);

    var user = new User()
    {
        Username = request.Username,
        Password = result
    };

    dbContext.Users.Add(user);
    await dbContext.SaveChangesAsync();

    return Results.NoContent();
})
.WithName("RegisterUser");

app.Run();

record CreateUser()
{
    public required string Username { get; init; }

    public required string Password { get; init; }
}