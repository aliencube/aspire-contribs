namespace Aspire.Contribs.Hosting.Java;

/// <summary>
/// This represents the options entity for configuring a Java application running in a container.
/// </summary>
public class JavaAppContainerResourceOptions
{
    /// <summary>
    /// Gets or sets the container registry.
    /// </summary>
    public string? ContainerRegistry { get; set; } = null;

    /// <summary>
    /// Gets or sets the container image name.
    /// </summary>
    public string? ContainerImageName { get; set; }

    /// <summary>
    /// Gets or sets the container image tag.
    /// </summary>
    public string ContainerImageTag { get; set; } = "latest";

    /// <summary>
    /// Gets or sets the port number.
    /// </summary>
    public int Port { get; set; } = 8080;

    /// <summary>
    /// Gets or sets the target port number.
    /// </summary>
    public int TargetPort { get; set; } = 8080;

    /// <summary>
    /// Gets or sets the OpenTelemetry Java Agent path.
    /// </summary>
    public string? OtelAgentPath { get; set; } = null;

    /// <summary>
    /// Gets or sets the arguments to pass to the Java application.
    /// </summary>
    public string[]? Args { get; set; } = null;
}
