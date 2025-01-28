var builder = DistributedApplication.CreateBuilder(args);

var storage = builder.AddAzureStorage("template-storage")
    .RunAsEmulator(r =>
    {
        r.WithLifetime(ContainerLifetime.Persistent)
        .WithContainerName("template")
        .WithImageTag("latest")
        .WithDataBindMount("c:/temp/azurite");
    })
    .AddTables("template-tables");

var api = builder.AddProject<Projects.Template_Api>("template-api")
    .WaitFor(storage)
    .WithReference(storage);

builder.Build().Run();
