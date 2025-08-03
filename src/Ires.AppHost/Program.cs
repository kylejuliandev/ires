var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.Ires_Api>("ires-api");

builder.Build().Run();
