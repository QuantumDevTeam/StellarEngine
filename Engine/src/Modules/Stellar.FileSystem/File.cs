using Stellar.Core.Quantization;
using Stellar.Kernel;

namespace Stellar.FileSystem;

/// <summary>
/// Quantum File with a content
/// </summary>
/// <param name="location">File location</param>
/// <param name="type">File type</param>
/// <param name="identifier">An unique identifier</param>
public class File(Location location, FileType type, IIdentifier? identifier = null)
    : MetaQuant(identifier),
        IEquatable<File>
{
    /// <summary>
    /// File location
    /// </summary>
    public readonly Location Location = location;

    /// <summary>
    /// File type
    /// </summary>
    public readonly FileType Type = type;

    /// <summary>
    /// Checking 2 files for identity
    /// </summary>
    /// <param name="other">Another file</param>
    /// <returns>file is identity?</returns>
    public bool Equals(File? other)
    {
        return Type.UID == other?.Type.UID && Location.UID == other.Location.UID;
    }

    /// <summary>
    /// Oper File Stream for reading from file
    /// </summary>
    /// <returns>File stream allowed for reading</returns>
    public FileStream OpenRead()
    {
        var stream = Location.Domain.FileSystem.OpenRead(Location);
        return new FileStream(this, stream);
    }

    /// <summary>
    /// Oper File Stream for writing in file
    /// </summary>
    /// <returns>File stream allowed for writing</returns>
    public FileStream OpenWrite()
    {
        var stream = Location.Domain.FileSystem.OpenWrite(Location);
        return new FileStream(this, stream);
    }

    /// <summary>
    /// Get information file information
    /// </summary>
    /// <returns>File information</returns>
    public IFileInfo GetInfo() => Location.Domain.FileSystem.GetInfo(Location);
}