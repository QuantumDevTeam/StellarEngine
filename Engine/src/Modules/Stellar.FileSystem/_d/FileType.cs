using Stellar.Core.Label;
using Stellar.Core.Quantization;
using Stellar.Kernel;
using Stellar.Kernel.FileSystem;
using Stellar.Kernel.Label;

namespace Stellar.FileSystem;

/// <summary>
/// Quantum File Type
/// </summary>
public class FileType
    : RegistrableMetaQuant<FileType>, IFileType
{
    /// <summary>
    /// Quantum File Type name
    /// </summary>
    public ILabel Label { get; }

    public FileType(string name, IIdentifier? identifier = null)
        : base(identifier)
    {
        Label = new Label(UID, name);
    }

    public override string ToString() => Label.Name;

    private bool Equals(FileType other)
    {
        return other.Label.Name == Label.Name;
    }

    public bool Equals(IFileType? obj)
    {
        return ReferenceEquals(this, obj) || obj is FileType other && Equals(other);
    }
}