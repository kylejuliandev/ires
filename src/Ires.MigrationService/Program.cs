using Ires.Data;
using Ires.MigrationService;
using Microsoft.EntityFrameworkCore;

var builder = Host.CreateApplicationBuilder(args);

var dbLocation = builder.Configuration["DB_LOCATION"]
    ?? throw new InvalidOperationException("DB_LOCATION configuration is missing");

builder.AddServiceDefaults();

builder.Services.AddOpenTelemetry()
    .WithTracing(tracing =>
    {
        tracing.AddSource(Worker.ActivitySourceName);
    });

builder.Services.AddDbContext<IresDbContext>(
    db => db.UseSqlite($"Data Source={dbLocation}"));

builder.Services.AddHostedService<Worker>();

var host = builder.Build();

host.Run();
