using Aspire.Contribs.Hosting.Java.Utils;
using Aspire.Hosting;
using Aspire.Hosting.ApplicationModel;

namespace Aspire.Contribs.Hosting.Java;

/// <summary>
/// Provides extension methods for adding Java applications to an <see cref="IDistributedApplicationBuilder"/>.
/// </summary>
public static class JavaAppHostingExtension
{
    /// <summary>
    /// Adds a Java application to the application model. Executes the containerized Java app.
    /// </summary>
    /// <param name="builder">The <see cref="IDistributedApplicationBuilder"/> to add the resource to.</param>
    /// <param name="name">The name of the resource.</param>
    /// <param name="options">The <see cref="JavaAppResourceOptions"/> to configure the Java application.</param>"
    /// <returns>A reference to the <see cref="IResourceBuilder{T}"/>.</returns>
    public static IResourceBuilder<JavaAppResource> AddJavaApp(
        this IDistributedApplicationBuilder builder,
        string name,
        JavaAppResourceOptions options)
    {
        if (string.IsNullOrWhiteSpace(options.ContainerImageName))
        {
            throw new ArgumentException("Container image name must be specified.", nameof(options));
        }

        var resource = new JavaAppResource(name);

        var rb = builder.AddResource(resource);
        if (options.ContainerRegistry is not null)
        {
            rb.WithImageRegistry(options.ContainerRegistry);
        }
        rb.WithImage(options.ContainerImageName)
          .WithImageTag(options.ContainerImageTag)
          .WithHttpEndpoint(port: options.Port, targetPort: options.TargetPort, name: JavaAppResource.HttpEndpointName)
          .WithJavaDefaults(options.OtelAgentPath);
        if (options.Args is { Length: > 0 })
        {
#pragma warning disable CS8604 // Possible null reference argument.
            rb.WithArgs(options.Args);
#pragma warning restore CS8604 // Possible null reference argument.
        }

        return rb;
    }

    /// <summary>
    /// Adds a Spring application to the application model. Executes the containerized Spring app.
    /// </summary>
    /// <param name="builder">The <see cref="IDistributedApplicationBuilder"/> to add the resource to.</param>
    /// <param name="name">The name of the resource.</param>
    /// <param name="options">The <see cref="JavaAppResourceOptions"/> to configure the Java application.</param>"
    /// <returns>A reference to the <see cref="IResourceBuilder{T}"/>.</returns>
    public static IResourceBuilder<JavaAppResource> AddSpringApp(
        this IDistributedApplicationBuilder builder,
        string name,
        JavaAppResourceOptions options)
    {
        return builder.AddJavaApp(name, options);
    }


    /// <summary>
    /// Adds a node application to the application model. Executes the npm command with the specified script name.
    /// </summary>
    /// <param name="builder">The <see cref="IDistributedApplicationBuilder"/> to add the resource to.</param>
    /// <param name="name">The name of the resource.</param>
    /// <param name="workingDirectory">The working directory to use for the command. If null, the working directory of the current process is used.</param>
    /// <param name="scriptName">The npm script to execute. Defaults to "start".</param>
    /// <param name="args">The arguments to pass to the command.</param>
    /// <returns>A reference to the <see cref="IResourceBuilder{T}"/>.</returns>
    public static IResourceBuilder<JavaAppExecutableResource> AddSpringApp(
        this IDistributedApplicationBuilder builder,
        string name,
        string workingDirectory,
        string appName = "target/app.jar",
        string otelAgentPath = "/agents",
        string[]? args = null)
    {
        string[] allArgs = args is { Length: > 0 }
            ? ["-jar", appName, .. args]
            : ["-jar", appName];

        workingDirectory = PathNormalizer.NormalizePathForCurrentPlatform(Path.Combine(builder.AppHostDirectory, workingDirectory));
        var resource = new JavaAppExecutableResource(name, "java", workingDirectory);

        return builder.AddResource(resource)
                      .WithJavaDefaults(otelAgentPath)
                      .WithHttpEndpoint(port: 8080, name: JavaAppResource.HttpEndpointName, isProxied: false)
                      .WithArgs(allArgs);
    }



    private static IResourceBuilder<JavaAppResource> WithJavaDefaults(
        this IResourceBuilder<JavaAppResource> builder,
        string? otelAgentPath = null) =>
        builder.WithOtlpExporter()
               .WithEnvironment("JAVA_TOOL_OPTIONS", $"-javaagent:{otelAgentPath?.TrimEnd('/')}/opentelemetry-javaagent.jar")
               ;

    private static IResourceBuilder<JavaAppExecutableResource> WithJavaDefaults(
        this IResourceBuilder<JavaAppExecutableResource> builder,
        string? otelAgentPath = null) =>
        builder.WithOtlpExporter()
               .WithEnvironment("JAVA_TOOL_OPTIONS", $"-javaagent:{otelAgentPath?.TrimEnd('/')}/opentelemetry-javaagent.jar")
               ;

}
