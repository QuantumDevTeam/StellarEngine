using Stellar.Kernel;
using Stellar.Kernel.FileSystem;
using Stellar.Kernel.Label;
using Stellar.Core.Quantization;
using Stellar.Core.Label;

namespace Stellar.FileSystem;

/// <summary>
/// Domain, identifies Location base
/// </summary>
public sealed class Domain
    : RegistrableMetaQuant<Domain>, IDomain
{
    /// <summary>
    /// Name of Domain
    /// </summary>
    public ILabel Label { get; }

    /// <summary>
    /// Type of Domain
    /// </summary>
    public DomainType Type { get; }

    /// <summary>
    /// Value of Domain
    /// </summary>
    public string Value { get; }

    public Domain(string name, DomainType type, string value, IIdentifier? identifier = null)
        : base(identifier)
    {
        Label = new Label(name, UID);
        Type = type;
        Value = value;
    }

    public override string ToString() => $"{Type}@{Label.Name}:{Value}";

    public override int GetHashCode()
    {
        return UID.GetHashCode();
    }

    private bool Equals(Domain other)
    {
        return other.Label.Name == Label.Name;
    }

    public bool Equals(IDomain? obj)
    {
        return ReferenceEquals(this, obj) || obj is Domain other && Equals(other);
    }
}