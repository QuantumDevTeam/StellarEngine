namespace Stellar.Core.Data.File.Systems;

public class DirectoryFileSystem : IFileSystem
{
    public string Name => "Directory";

    public bool Exists(Location location)
    {
        var fullPath = Path.Combine(location.Domain.Value, location.Path);
        return System.IO.File.Exists(fullPath);
    }

    public Stream OpenRead(Location location)
    {
        var fullPath = Path.Combine(location.Domain.Value, location.Path);
        return System.IO.File.OpenRead(fullPath);
    }

    public Stream OpenWrite(Location location)
    {
        var fullPath = Path.Combine(location.Domain.Value, location.Path);
        return System.IO.File.OpenWrite(fullPath);
    }
}