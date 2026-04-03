using Stellar.Core.Quantization;
using Stellar.Kernel.FileSystem;
using Stellar.Kernel.FileSystem.Provider;

namespace Stellar.FileSystem;

public class FileStream(IFile file, Stream stream)
    : MetaQuant, IFileStream
{
    /// <summary>
    /// File himself
    /// </summary>
    public IFile File { get; } = file;

    /// <summary>
    /// Stream for File
    /// </summary>
    public Stream Stream { get; } = stream;

    public FileStream(IFile file, IFileProviderFactory fileProviderFactory, FileAccess access)
        : this(file, fileProviderFactory.GetFileProvider(file.Location.Domain).Open(file.Location, access))
    {
    }

    public void Dispose()
    {
        Stream.Dispose();
    }

    private bool Equals(FileStream other)
    {
        return other.File == File;
    }

    public bool Equals(IFileStream? obj)
    {
        return ReferenceEquals(this, obj) || obj is FileStream other && Equals(other);
    }
}