using Stellar.Core.Quantization;
using Stellar.Kernel;
using Stellar.Kernel.FileSystem;

namespace Stellar.FileSystem;

/// <summary>
/// Quantum File Location
/// </summary>
/// <param name="domain">Location Domain (base)</param>
/// <param name="path">path in Domain</param>
/// <param name="identifier">An unique identifier</param>
public sealed class Location(Domain domain, string path, IIdentifier? identifier = null)
    : MetaQuant(identifier), ILocation
{
    /// <summary>
    /// Domain of this Location
    /// </summary>
    public IDomain Domain { get; } = domain;

    /// <summary>
    /// Location path in Domain
    /// </summary>
    public string Path { get; } = path;

    /// <summary>
    /// A valid string associated with this Location
    /// </summary>
    /// <returns>String format `{Domain}://{Path}`</returns>
    public override string ToString() => $"{Domain}://{Path}";

    public override int GetHashCode()
    {
        return UID.GetHashCode();
    }

    private bool Equals(Location other)
    {
        return other.Domain == Domain && other.Path == Path;
    }

    public bool Equals(ILocation? obj)
    {
        return ReferenceEquals(this, obj) || obj is Location other && Equals(other);
    }
}