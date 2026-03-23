using Stellar.Core.Quantization;
using Stellar.Kernel;

namespace Stellar.FileSystem;

/// <summary>
/// Quantum File Type
/// </summary>
/// <param name="name">Type Name (his identifier in Data Container)</param>
/// <param name="identifier">An unique identifier</param>
public class FileType(string name, IIdentifier? identifier = null)
    : RegistrableMetaQuant<FileType>(identifier)
{
    /// <summary>
    /// Quantum File Type name
    /// </summary>
    public readonly string Name = name;


    public override string ToString() => Name;
}