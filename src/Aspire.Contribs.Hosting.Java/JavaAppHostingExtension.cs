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
    /// <param name="containerImageName">The name of the container image to use.</param>
    /// <param name="containerImageTag">The tag of the container image to use. Defaults to "latest".</param>
    /// <param name="containerRegistry">The container registry to use. If null, the default registry is used.</param>
    /// <param name="port">The port to expose on the container. Defaults to 8080.</param>
    /// <param name="targetPort">The target port to expose on the container. Defaults to 8080.</param>
    /// <param name="agentPath">The path to the OpenTelemetry Java agent. Default is null</param>
    /// <param name="args">The arguments to pass to the command.</param>
    /// <returns>A reference to the <see cref="IResourceBuilder{T}"/>.</returns>
    public static IResourceBuilder<JavaAppResource> AddJavaApp(
        this IDistributedApplicationBuilder builder,
        string name,
        string containerImageName,
        string containerImageTag = "latest",
        string? containerRegistry = null,
        int port = 8080,
        int targetPort = 8080,
        string? agentPath = null,
        string[]? args = null)
    {
        var resource = new JavaAppResource(name);

        var rb = builder.AddResource(resource);
        if (containerRegistry is not null)
        {
            rb.WithImageRegistry(containerRegistry);
        }
        rb.WithImage(containerImageName)
          .WithImageTag(containerImageTag)
          .WithHttpEndpoint(port: port, targetPort: targetPort, name: JavaAppResource.HttpEndpointName)
          .WithJavaDefaults(agentPath);
        if (args is { Length: > 0 })
        {
            rb.WithArgs(args);
        }

        return rb;
    }

    /// <summary>
    /// Adds a Spring application to the application model. Executes the containerized Spring app.
    /// </summary>
    /// <param name="builder">The <see cref="IDistributedApplicationBuilder"/> to add the resource to.</param>
    /// <param name="name">The name of the resource.</param>
    /// <param name="containerImageName">The name of the container image to use.</param>
    /// <param name="containerImageTag">The tag of the container image to use. Defaults to "latest".</param>
    /// <param name="containerRegistry">The container registry to use. If null, the default registry is used.</param>
    /// <param name="port">The port to expose on the container. Defaults to 8080.</param>
    /// <param name="targetPort">The target port to expose on the container. Defaults to 8080.</param>
    /// <param name="agentPath">The path to the OpenTelemetry Java agent. Default is null</param>
    /// <param name="args">The arguments to pass to the command.</param>
    /// <returns>A reference to the <see cref="IResourceBuilder{T}"/>.</returns>
    public static IResourceBuilder<JavaAppResource> AddSpringApp(
        this IDistributedApplicationBuilder builder,
        string name,
        string containerImageName,
        string containerImageTag = "latest",
        string? containerRegistry = null,
        int port = 8080,
        int targetPort = 8080,
        string? agentPath = null,
        string[]? args = null)
    {
        return builder.AddJavaApp(name, containerImageName, containerImageTag, containerRegistry, port, targetPort, agentPath, args);
    }

    private static IResourceBuilder<JavaAppResource> WithJavaDefaults(
        this IResourceBuilder<JavaAppResource> builder,
        string? agentPath = null) =>
        builder.WithOtlpExporter()
               .WithEnvironment("JAVA_TOOL_OPTIONS", $"-javaagent:{agentPath?.TrimEnd('/')}/opentelemetry-javaagent.jar")
               ;
}
