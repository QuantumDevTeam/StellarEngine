using System.Reflection;

namespace Stellar.FileSystem.Systems;

/// <summary>
/// File system that reads embedded resources from a .NET assembly.
/// The domain value must be the full path to the assembly file (e.g., "C:\MyLib.dll").
/// </summary>
public class AssemblyFileSystem : IFileSystem
{
    private class AssemblyFileInfo(string path, long length) : IFileInfo
    {
        public string Name { get; } = Path.GetFileName(path);
        public string FullPath { get; } = path;
        public long Length { get; } = length;
        public DateTime? CreationTimeUtc => null;
        public DateTime? LastWriteTimeUtc => null;
        public bool IsDirectory => false;
        public bool Exists => true;
    }

    public string Name => "Assembly";

    private static Assembly LoadAssembly(string assemblyPath)
    {
        try
        {
            return Assembly.LoadFrom(assemblyPath);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Failed to load assembly from '{assemblyPath}'.", ex);
        }
    }

    public List<Location> ExistsAny(Location locationPattern)
    {
        List<Location> locations = (
            from resource in LoadAssembly(locationPattern.Domain.Value).GetManifestResourceNames()
            where resource.StartsWith(locationPattern.Path, StringComparison.Ordinal)
            select new Location(locationPattern.Domain, resource)
        ).ToList();

        return locations.ToList();
    }

    public bool Exists(Location location)
    {
        var assembly = LoadAssembly(location.Domain.Value);
        // Resource name is typically the full path inside the assembly (e.g., "Folder.File.ext")
        // We'll treat location.Path as the resource name (case‑sensitive).
        return assembly.GetManifestResourceInfo(location.Path) != null;
    }

    public Stream OpenRead(Location location)
    {
        var assembly = LoadAssembly(location.Domain.Value);
        var stream = assembly.GetManifestResourceStream(location.Path);
        if (stream == null)
            throw new FileNotFoundException(
                $"Embedded resource '{location.Path}' not found in assembly '{location.Domain.Value}'.");
        return stream;
    }

    public Stream OpenWrite(Location location)
    {
        throw new NotSupportedException("Writing to assembly resources is not supported.");
    }

    public IFileInfo GetInfo(Location location)
    {
        var assembly = LoadAssembly(location.Domain.Value);
        var stream = assembly.GetManifestResourceStream(location.Path);
        if (stream == null)
            throw new FileNotFoundException($"Embedded resource '{location.Path}' not found.");

        return new AssemblyFileInfo(location.Path, stream.Length);
    }
}