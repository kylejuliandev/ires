using Ires.Data;
using Ires.MigrationService;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddOpenTelemetry()
    .WithTracing(tracing => tracing.AddSource(Worker.ActivitySourceName));

builder.AddNpgsqlDbContext<IresDbContext>("iresdb");

builder.Services.AddHostedService<Worker>();

var host = builder.Build();

host.Run();
