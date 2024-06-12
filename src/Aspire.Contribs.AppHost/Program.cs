using Aspire.Contribs.Hosting.Java;

var builder = DistributedApplication.CreateBuilder(args);

var apiapp = builder.AddProject<Projects.Aspire_Contribs_ApiApp>("apiapp");
var containerapp = builder.AddSpringApp("containerapp",
                                     new JavaAppContainerResourceOptions()
                                     {
                                         ContainerImageName = "aspire-contribs/spring-maven-sample",
                                         OtelAgentPath = "/agents"
                                     });

var executableapp = builder.AddSpringApp("executableapp",
                                     workingDirectory: "../Aspire.Contribs.Spring.Maven",
                                     new JavaAppExecutableResourceOptions()
                                     {
                                         ApplicationName = "target/spring-maven-0.0.1-SNAPSHOT.jar",
                                         Port = 8085,
                                         OtelAgentPath = "../../agents",
                                     });

builder.AddProject<Projects.Aspire_Contribs_WebApp>("webapp")
       .WithReference(containerapp)
       .WithReference(executableapp)
       .WithReference(apiapp);

builder.Build().Run();
