var builder = DistributedApplication.CreateBuilder(args);

var temp = Path.Combine(Path.GetTempPath(), "Ires");
Directory.CreateDirectory(temp);

var sqliteFilePath = Path.Combine(temp, "ires.db");

builder.AddProject<Projects.Ires_Frontend>("frontend")
    .WithEnvironment("DB_LOCATION", sqliteFilePath);

builder.AddProject<Projects.Ires_Helper>("ires-helper")
    .WithEnvironment("DB_LOCATION", sqliteFilePath);

builder.Build().Run();
