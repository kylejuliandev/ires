var builder = DistributedApplication.CreateBuilder(args);

var temp = Path.Combine(Path.GetTempPath(), "Ires");
Directory.CreateDirectory(temp);

var sqliteFilePath = Path.Combine(temp, "ires.db");

var migration = builder.AddProject<Projects.Ires_MigrationService>("migrationservice")
    .WithEnvironment("DB_LOCATION", sqliteFilePath);

builder.AddProject<Projects.Ires_Frontend>("frontend")
    .WithEnvironment("DB_LOCATION", sqliteFilePath)
    .WaitForCompletion(migration);

builder.Build().Run();
