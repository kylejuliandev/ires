var builder = DistributedApplication.CreateBuilder(args);

var temp = Path.Combine(Path.GetTempPath(), "Ires");
Directory.CreateDirectory(temp);

var sqliteFilePath = Path.Combine(temp, "ires.db");

builder.AddProject<Projects.Ires_Frontend>("frontend")
    .WithEnvironment("DB_LOCATION", sqliteFilePath)
    .WithEnvironment("PERMIT_SIGNUP", "true");

builder.Build().Run();
