using Aspire.Contribs.Hosting.Java;

var builder = DistributedApplication.CreateBuilder(args);

var apiapp = builder.AddProject<Projects.Aspire_Contribs_ApiApp>("apiapp");
// var springapp = builder.AddSpringApp("springapp",
//                                      new JavaAppResourceOptions()
//                                      {
//                                          ContainerImageName = "aspire-contribs/spring-maven-sample",
//                                          OtelAgentPath = "/agents"
//                                      });

var springapp = builder.AddSpringApp("springapp",
                                     workingDirectory: "../Aspire.Contribs.Spring.Maven",
                                     appName: "target/spring-maven-0.0.1-SNAPSHOT.jar",
                                     otelAgentPath: "../../agents",
                                     args: [ "-Dotel.exporter.otlp.endpoint=https://localhost:21000" ]);

// var springapp = builder.AddExecutable("springapp",
//                                      command: "java",
//                                      workingDirectory: "../Aspire.Contribs.Spring.Maven",
//                                      args: ["-jar", "target/spring-maven-0.0.1-SNAPSHOT.jar"])
//                        .WithEnvironment("JAVA_TOOL_OPTIONS", $"-javaagent:../../agents/opentelemetry-javaagent.jar")
//                        .WithHttpEndpoint(port: 8080, isProxied: false);

builder.AddProject<Projects.Aspire_Contribs_WebApp>("webapp")
       .WithReference(springapp)
       .WithReference(apiapp);

builder.Build().Run();
