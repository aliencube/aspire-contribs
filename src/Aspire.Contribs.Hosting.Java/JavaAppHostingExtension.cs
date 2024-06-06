using Aspire.Contribs.Hosting.Java.Utils;
using Aspire.Hosting;
using Aspire.Hosting.ApplicationModel;

using Microsoft.Extensions.Hosting;

namespace Aspire.Contribs.Hosting.Java;

/// <summary>
/// Provides extension methods for adding Java applications to an <see cref="IDistributedApplicationBuilder"/>.
/// </summary>
public static class JavaAppHostingExtension
{
    /// <summary>
    /// Adds a Java application to the application model. Java should available on the PATH.
    /// </summary>
    /// <param name="builder">The <see cref="IDistributedApplicationBuilder"/> to add the resource to.</param>
    /// <param name="name">The name of the resource.</param>
    /// <param name="scriptPath">The path to the script that Java will execute.</param>
    /// <param name="workingDirectory">The working directory to use for the command. If null, the working directory of the current process is used.</param>
    /// <param name="args">The arguments to pass to the command.</param>
    /// <returns>A reference to the <see cref="IResourceBuilder{T}"/>.</returns>
    public static IResourceBuilder<JavaAppResource> AddJavaApp(this IDistributedApplicationBuilder builder, string name, string scriptPath, string? workingDirectory = null, string[]? args = null)
    {
        args ??= [];
        string[] effectiveArgs = [scriptPath, .. args];
        workingDirectory ??= Path.GetDirectoryName(scriptPath)!;
        workingDirectory = Path.Combine(builder.AppHostDirectory, workingDirectory)
                               .NormalizePathForCurrentPlatform();

        var resource = new JavaAppResource(name, "java", workingDirectory);

        return builder.AddResource(resource)
                      .WithJavaDefaults()
                      .WithArgs(effectiveArgs);
    }

    /// <summary>
    /// Adds a Spring application to the application model. Executes the Java command with the specified script name.
    /// </summary>
    /// <param name="builder">The <see cref="IDistributedApplicationBuilder"/> to add the resource to.</param>
    /// <param name="name">The name of the resource.</param>
    /// <param name="workingDirectory">The working directory to use for the command. If null, the working directory of the current process is used.</param>
    /// <param name="scriptName">The npm script to execute. Defaults to "start".</param>
    /// <param name="args">The arguments to pass to the command.</param>
    /// <returns>A reference to the <see cref="IResourceBuilder{T}"/>.</returns>
    public static IResourceBuilder<JavaAppResource> AddSpringApp(this IDistributedApplicationBuilder builder, string name, string workingDirectory, string scriptName = "start", string[]? args = null)
    {
        string[] allArgs = args is { Length: > 0 }
            ? ["run", scriptName, "--", .. args]
            : ["run", scriptName];

        workingDirectory = Path.Combine(builder.AppHostDirectory, workingDirectory)
                               .NormalizePathForCurrentPlatform();
        var resource = new JavaAppResource(name, "java", workingDirectory);

        return builder.AddResource(resource)
                      .WithJavaDefaults()
                      .WithArgs(allArgs);
    }

    private static IResourceBuilder<JavaAppResource> WithJavaDefaults(this IResourceBuilder<JavaAppResource> builder) =>
        builder.WithOtlpExporter()
               //.WithEnvironment("NODE_ENV", builder.ApplicationBuilder.Environment.IsDevelopment() ? "development" : "production")
               ;
}
