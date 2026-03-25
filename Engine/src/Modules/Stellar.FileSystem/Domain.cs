using Stellar.Kernel;
using Stellar.Core.Quantization;
using Stellar.Kernel.FileSystem;

namespace Stellar.FileSystem;

/// <summary>
/// Domain, identifies Location base
/// </summary>
/// <param name="type">Type of Domain</param>
/// <param name="value">Name of Domain (his identifier in a DataContainer)</param>
/// <param name="identifier">An unique identifier</param>
public sealed class Domain(string name, DomainType type, string value, IIdentifier? identifier = null)
    : RegistrableMetaQuant<Domain>(identifier), IDomain
{
    /// <summary>
    /// Name of Domain
    /// </summary>
    public string Name { get; } = name;

    /// <summary>
    /// Type of Domain
    /// </summary>
    public DomainType Type { get; } = type;

    /// <summary>
    /// Value of Domain
    /// </summary>
    public string Value { get; } = value;

    public override string ToString() => $"{Type}@{Name}:{Value}";

    public override int GetHashCode()
    {
        return UID.GetHashCode();
    }

    private bool Equals(Domain other)
    {
        return other.Name == Name;
    }

    public bool Equals(IDomain? obj)
    {
        return ReferenceEquals(this, obj) || obj is Domain other && Equals(other);
    }
}