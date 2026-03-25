using Stellar.Core.Quantization;
using Stellar.Kernel;
using Stellar.Kernel.FileSystem;

namespace Stellar.FileSystem;

/// <summary>
/// Quantum File Type
/// </summary>
/// <param name="name">Type Name (his identifier in Data Container)</param>
/// <param name="identifier">An unique identifier</param>
public class FileType(string name, IIdentifier? identifier = null)
    : RegistrableMetaQuant<FileType>(identifier), IFileType
{
    /// <summary>
    /// Quantum File Type name
    /// </summary>
    public string Name { get; } = name;

    public override string ToString() => Name;

    public override int GetHashCode()
    {
        return UID.GetHashCode();
    }

    private bool Equals(FileType other)
    {
        return other.Name == Name;
    }

    public bool Equals(IFileType? obj)
    {
        return ReferenceEquals(this, obj) || obj is FileType other && Equals(other);
    }
}