var builder = DistributedApplication.CreateBuilder(args);

var apiapp = builder.AddProject<Projects.Aspire_Contribs_ApiApp>("apiapp");

builder.AddProject<Projects.Aspire_Contribs_WebApp>("webapp")
       .WithReference(apiapp);

builder.Build().Run();
