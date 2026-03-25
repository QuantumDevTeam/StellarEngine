// ReSharper disable RedundantNameQualifier

using Stellar.Kernel.FileSystem;
using Stellar.Kernel.FileSystem.Provider;

namespace Stellar.FileSystem.Providers;

public class FileSystemFileProvider : IFileProvider
{
    #region Deps

    private string GetFullPath(ILocation location) => Path.Combine(location.Domain.Value, location.Path);

    public class FSFileInfo(System.IO.FileInfo fileInfo) : IFileInfo
    {
        public string Name { get; } = fileInfo.Name;
        public string FullPath { get; } = fileInfo.FullName;
        public long Length { get; } = fileInfo.Length;
        public DateTime? CreationTimeUtc { get; } = fileInfo.CreationTimeUtc;
        public DateTime? LastWriteTimeUtc { get; } = fileInfo.LastWriteTimeUtc;
    }

    #endregion

    private static readonly Lock _lock = new();

    public bool CanHandle(IDomain domain) => domain.Type == DomainType.Directory;

    public bool Exists(ILocation location) => System.IO.File.Exists(GetFullPath(location));

    public IFileInfo GetFileInfo(ILocation location) => new FSFileInfo(new System.IO.FileInfo(GetFullPath(location)));

    public Stream OpenRead(ILocation location)
    {
        lock (_lock)
        {
            var stream = System.IO.File.OpenRead(GetFullPath(location));

            return stream is { CanRead: true }
                ? stream
                : throw new FileNotFoundException(
                    $"Resource `{location}` not found or can not be opened for reading.");
        }
    }

    public Stream OpenWrite(ILocation location)
    {
        lock (_lock)
        {
            var stream = System.IO.File.OpenWrite(GetFullPath(location));

            return stream is { CanWrite: true }
                ? stream
                : throw new FileNotFoundException(
                    $"Resource `{location}` not found or can not be opened for writing.");
        }
    }

    public Stream OpenReadWrite(ILocation location)
    {
        lock (_lock)
        {
            var stream = System.IO.File.Open(GetFullPath(location), FileMode.OpenOrCreate, FileAccess.ReadWrite);

            return stream is { CanRead: true, CanWrite: true }
                ? stream
                : throw new FileNotFoundException(
                    $"Resource `{location}` not found or can not be opened for reading and writing.");
        }
    }

    public Stream Open(ILocation location, FileAccess access)
    {
        return access switch
        {
            FileAccess.Read => OpenRead(location),
            FileAccess.Write => OpenWrite(location),
            FileAccess.ReadWrite => OpenReadWrite(location),
            _ => throw new ArgumentOutOfRangeException(nameof(access), access, null)
        };
    }
}