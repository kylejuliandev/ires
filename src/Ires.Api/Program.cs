using Asp.Versioning;
using Ires.Api;
using Ires.Api.Endpoints.Person;
using Ires.Data;

var builder = WebApplication.CreateSlimBuilder(args);

builder.AddServiceDefaults();

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, IresJsonSerializerContext.Default);
});

builder.Services.AddOpenApi();

builder.Services.AddIresApiVersioning();

builder.AddNpgsqlDbContext<IresDbContext>("iresdb");

var app = builder.Build();

app.MapDefaultEndpoints();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

var apiVersion = app.NewApiVersionSet()
    .HasApiVersion(new ApiVersion(1.0))
    .ReportApiVersions()
    .Build();

var endpoints = app.MapGroup("api/v{v:apiVersion}")
    .WithApiVersionSet(apiVersion);

endpoints.MapPersonEndpoints();

app.Run();