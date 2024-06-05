namespace Aspire.Contribs.Hosting.Java.Utils;

internal static class PathNormalizer
{
    public static string NormalizePathForCurrentPlatform(this string path)
    {
        if (string.IsNullOrEmpty(path))
        {
            return path;
        }

        // Fix slashes
        path = path.Replace('\\', Path.DirectorySeparatorChar).Replace('/', Path.DirectorySeparatorChar);

        return Path.GetFullPath(path);
    }
}
