using Ires.Data;
using Ires.MigrationService;
using Microsoft.EntityFrameworkCore;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddOpenTelemetry()
    .WithTracing(tracing =>
    {
        tracing.AddSource(Worker.ActivitySourceName);
    });

builder.Services.AddDbContext<IresDbContext>(
    db => db.UseSqlite($"Data Source={builder.Configuration["DB_LOCATION"]}"));

builder.Services.AddHostedService<Worker>();

var host = builder.Build();

host.Run();
