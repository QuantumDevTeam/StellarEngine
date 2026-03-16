using Stellar.Core.Quantization;
using Stellar.Kernel;

namespace Stellar.Core.Data.File;

/// <summary>
/// Domain, identifies Location base
/// </summary>
/// <param name="type">Type of Domain</param>
/// <param name="value">Name of Domain (his identifier in a DataContainer)</param>
/// <param name="identifier">An unique identifier</param>
public sealed class Domain(DomainType type, string value, IFileSystem fileSystem, IIdentifier? identifier = null)
    : RegistrableMetaQuant<Domain>(identifier)
{
    /// <summary>
    /// Type of Domain
    /// </summary>
    public readonly DomainType Type = type;

    /// <summary>
    /// Name of Domain
    /// </summary>
    public readonly string Value = value;

    /// <summary>
    /// File System for operating with this Domain
    /// </summary>
    public readonly IFileSystem FileSystem = fileSystem;

    public override string ToString() => $"{Type}:{Value}";
}