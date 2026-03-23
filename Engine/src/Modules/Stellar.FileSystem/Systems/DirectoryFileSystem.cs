namespace Stellar.FileSystem.Systems;

public class DirectoryFileSystem : IFileSystem
{
    private class DirectoryFileInfo(string path, FileInfo fileInfo) : IFileInfo
    {
        public string Name => fileInfo.Name;
        public string FullPath => path;
        public long Length => fileInfo.Exists ? fileInfo.Length : -1;
        public DateTime? CreationTimeUtc => fileInfo.Exists ? fileInfo.CreationTimeUtc : null;
        public DateTime? LastWriteTimeUtc => fileInfo.Exists ? fileInfo.LastWriteTimeUtc : null;
        public bool IsDirectory => (fileInfo.Attributes & FileAttributes.Directory) != 0;
        public bool Exists => fileInfo.Exists;
    }

    public string Name => "Directory";

    public string GetFullPath(Location location) => Path.Combine(location.Domain.Value, location.Path);

    public List<Location> ExistsAny(Location locationPattern)
    {
        List<Location> locations = (
            from resource in Directory.EnumerateFiles(GetFullPath(locationPattern))
            where resource.StartsWith(locationPattern.Path, StringComparison.Ordinal)
            select new Location(locationPattern.Domain, resource)
        ).ToList();

        return locations.ToList();
    }

    public bool Exists(Location location) => System.IO.File.Exists(GetFullPath(location));

    public Stream OpenRead(Location location) => System.IO.File.OpenRead(GetFullPath(location));

    public Stream OpenWrite(Location location)
    {
        var fullPath = GetFullPath(location);
        Directory.CreateDirectory(Path.GetDirectoryName(fullPath)!);
        return System.IO.File.OpenWrite(fullPath);
    }

    public IFileInfo GetInfo(Location location)
    {
        var fullPath = GetFullPath(location);
        var fileInfo = new FileInfo(fullPath);
        return new DirectoryFileInfo(location.Path, fileInfo);
    }
}