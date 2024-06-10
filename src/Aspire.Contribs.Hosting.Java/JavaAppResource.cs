using Aspire.Hosting;
using Aspire.Hosting.ApplicationModel;

namespace Aspire.Contribs.Hosting.Java;

/// <summary>
/// A resource that represents a Java application.
/// </summary>
/// <param name="name">The name of the resource.</param>
public class JavaAppResource(string name)
    : ContainerResource(name), IResourceWithServiceDiscovery
{
    internal const string HttpEndpointName = "http";
}
