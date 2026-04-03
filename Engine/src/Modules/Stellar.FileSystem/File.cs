using Stellar.Kernel;
using Stellar.Kernel.FileSystem;
using Stellar.Core.Quantization;

namespace Stellar.FileSystem;

/// <summary>
/// Quantum File with a content
/// </summary>
/// <param name="location">File location</param>
/// <param name="type">File type</param>
/// <param name="identifier">An unique identifier</param>
public class File(Location location, FileType type, IIdentifier? identifier = null)
    : MetaQuant(identifier), IFile
{
    /// <summary>
    /// File location
    /// </summary>
    public ILocation Location { get; } = location;

    /// <summary>
    /// File type
    /// </summary>
    public IFileType Type { get; } = type;

    private bool Equals(File other)
    {
        return other.Location == Location;
    }

    public bool Equals(IFile? obj)
    {
        return ReferenceEquals(this, obj) || obj is File other && Equals(other);
    }
}