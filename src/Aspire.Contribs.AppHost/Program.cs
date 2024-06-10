using Aspire.Contribs.Hosting.Java;

var builder = DistributedApplication.CreateBuilder(args);

var apiapp = builder.AddProject<Projects.Aspire_Contribs_ApiApp>("apiapp");
var springapp = builder.AddSpringApp("springapp",
                                     new JavaAppResourceOptions()
                                     {
                                         ContainerImageName = "aspire-contribs/spring-maven-sample",
                                         OtelAgentPath = "/agents"
                                     });

builder.AddProject<Projects.Aspire_Contribs_WebApp>("webapp")
       .WithReference(apiapp)
       .WithReference(springapp);

builder.Build().Run();
