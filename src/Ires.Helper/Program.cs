using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapPost("/hashpassword", ([FromBody] HashPasswordRequest request) =>
{
    var hasher = new PasswordHasher<string>();

    var result = hasher.HashPassword("user", request.Password);

    return Results.Ok(new { HashedPassword = result });
})
.WithName("HashPassword");

app.Run();

record HashPasswordRequest()
{
    public required string Password { get; init; }
}