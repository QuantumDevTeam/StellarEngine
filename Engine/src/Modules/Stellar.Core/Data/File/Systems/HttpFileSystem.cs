namespace Stellar.Core.Data.File.Systems;

/// <summary>
/// File system for HTTP/HTTPS resources.
/// Domain value must be the host (e.g., "example.com" or "localhost:8080").
/// Path is the resource path including leading slash (e.g., "/api/data").
/// </summary>
public class HttpFileSystem(DomainType type) : IFileSystem
{
    private class HttpFileInfo(string path) : IFileInfo
    {
        public string Name { get; } = Path.GetFileName(path);
        public string FullPath { get; } = path;
        public long Length => -1;
        public DateTime? CreationTimeUtc => null;
        public DateTime? LastWriteTimeUtc => null;
        public bool IsDirectory => false;
        public bool Exists => true; // We assume existence, otherwise OpenRead would fail
    }

    private readonly string _scheme = type.ToString().ToLowerInvariant(); // "http" or "https"
    private readonly HttpClient _client = new();

    public string Name => _scheme.ToUpperInvariant();

    public bool Exists(Location location)
    {
        // Not implemented – would require a HEAD request.
        throw new NotImplementedException("HTTP existence check (HEAD) is not implemented.");
    }

    public Stream OpenRead(Location location)
    {
        var url = $"{_scheme}://{location.Domain.Value}{location.Path}";
        return _client.GetStreamAsync(url).GetAwaiter().GetResult();
    }

    public Stream OpenWrite(Location location)
    {
        throw new NotSupportedException("HTTP write is not supported.");
    }

    public IFileInfo GetInfo(Location location)
    {
        // Minimal info: only name and path; length and timestamps unknown.
        return new HttpFileInfo(location.Path);
    }
}