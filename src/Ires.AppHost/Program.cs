var builder = DistributedApplication.CreateBuilder(args);

var database = builder.AddPostgres("pgsql")
    .WithPgAdmin(c => 
        c.WithLifetime(ContainerLifetime.Persistent)
    )
    .WithLifetime(ContainerLifetime.Persistent)
    .AddDatabase("iresdb");

builder.AddProject<Projects.Ires_Api>("ires-api")
    .WithReference(database)
    .WaitFor(database);

builder.Build().Run();
