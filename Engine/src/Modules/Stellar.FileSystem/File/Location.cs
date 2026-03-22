using Stellar.Core.Quantization;
using Stellar.Kernel;

namespace Stellar.FileSystem.File;

/// <summary>
/// Quantum File Location
/// </summary>
/// <param name="domain">Location Domain (base)</param>
/// <param name="path">path in Domain</param>
/// <param name="identifier">An unique identifier</param>
public sealed class Location(Domain domain, string path, IIdentifier? identifier = null)
    : RegistrableMetaQuant<Location>(identifier)
{
    /// <summary>
    /// Domain of this Location
    /// </summary>
    public readonly Domain Domain = domain;

    /// <summary>
    /// Location path in Domain
    /// </summary>
    public readonly string Path = path;

    /// <summary>
    /// A valid string associated with this Location
    /// </summary>
    /// <returns>String format `{Domain}://{Path}`</returns>
    public override string ToString() => $"{Domain}://{Path}";
}