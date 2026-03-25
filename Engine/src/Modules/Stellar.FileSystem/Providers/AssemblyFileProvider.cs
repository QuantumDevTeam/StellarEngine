using System.Reflection;
using Stellar.Kernel.FileSystem;
using Stellar.Kernel.FileSystem.Provider;

namespace Stellar.FileSystem.Providers;

public class AssemblyFileProvider(Assembly assembly) : IFileProvider
{
    #region Deps

    public class AFileInfo(string name, string path, long length) : IFileInfo
    {
        public string Name { get; } = name;
        public string FullPath { get; } = path;
        public long Length { get; } = length;
        public DateTime? CreationTimeUtc { get; } = null;
        public DateTime? LastWriteTimeUtc { get; } = null;
    }

    #endregion

    public bool CanHandle(IDomain domain) => domain.Type == DomainType.Assembly;

    public bool Exists(ILocation location) => assembly.GetManifestResourceInfo(location.Path) != null;

    public IFileInfo GetFileInfo(ILocation location) => new AFileInfo(
        assembly.GetManifestResourceInfo(location.Path)?.FileName ?? "", location.Path,
        assembly.GetManifestResourceStream(location.Path)?.Length ?? -1);

    public Stream OpenRead(ILocation location)
    {
        var stream = assembly.GetManifestResourceStream(location.Path);
        return stream is { CanRead: true }
            ? stream
            : throw new FileNotFoundException(
                $"Resource `{location}` not found or can not be opened for reading.");
    }

    public Stream OpenWrite(ILocation location)
    {
        var stream = assembly.GetManifestResourceStream(location.Path);
        return stream is { CanWrite: true }
            ? stream
            : throw new FileNotFoundException(
                $"Resource `{location}` not found or can not be opened for writing.");
    }

    public Stream OpenReadWrite(ILocation location)
    {
        var stream = assembly.GetManifestResourceStream(location.Path);
        return stream is { CanRead: true, CanWrite: true }
            ? stream
            : throw new FileNotFoundException(
                $"Resource `{location}` not found or can not be opened for reading and writing.");
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