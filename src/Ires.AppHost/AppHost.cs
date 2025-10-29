var builder = DistributedApplication.CreateBuilder(args);

var database = builder.AddPostgres("pgsql")
    .WithPgAdmin(c => 
        c.WithLifetime(ContainerLifetime.Persistent)
    )
    .WithLifetime(ContainerLifetime.Persistent)
    .AddDatabase("iresdb");

var migration = builder.AddProject<Projects.Ires_MigrationService>("migrationservice")
    .WithReference(database)
    .WaitFor(database);

builder.AddProject<Projects.Ires_Frontend>("frontend")
    .WithReference(database)
    .WaitFor(database)
    .WaitForCompletion(migration);

builder.Build().Run();
