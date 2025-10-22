var builder = DistributedApplication.CreateBuilder(args);

var database = builder.AddPostgres("pgsql")
    .WithPgAdmin(c => 
        c.WithLifetime(ContainerLifetime.Persistent)
    )
    .WithLifetime(ContainerLifetime.Persistent)
    .AddDatabase("iresdb");

var migration = builder.AddProject<Projects.Ires_MigrationService>("ires-migrationservice")
    .WithReference(database)
    .WaitFor(database);

var api = builder.AddProject<Projects.Ires_Api>("iresapi")
    .WithReference(database)
    .WaitFor(database)
    .WaitForCompletion(migration);

var frontend = builder.AddNpmApp("reactvite", "../frontend/ires-vite")
    .WithEnvironment("BROWSER", "none")
    .WithHttpEndpoint(env: "VITE_PORT")
    .WithReference(api)
    .WithExternalHttpEndpoints();

builder.Build().Run();
