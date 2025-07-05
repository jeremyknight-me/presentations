using Aspire.AppHost;

var builder = DistributedApplication.CreateBuilder(args);

var sql = builder.AddSqlServer(ResourceNames.SqlServer)
    .WithLifetime(ContainerLifetime.Persistent);

var db = sql.AddDatabase(ResourceNames.Database);

builder.AddProject<Projects.SampleWeb_MigrationService>(ResourceNames.MigrationService)
    .WithReference(db)
    .WaitFor(db);

builder.AddProject<Projects.SampleWeb>(ResourceNames.WebApi)
    .WithReference(db)
    .WaitFor(db);

builder.Build().Run();
