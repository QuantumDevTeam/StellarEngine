using Stellar.Core.Quantization;
using Stellar.Kernel;

namespace Stellar.Core.Data.File;

public sealed class Domain(DomainType type, string name, string value, IIdentifier? identifier = null)
    : RegistrableMetaQuant<Domain>(identifier)
{
    public readonly DomainType Type = type;
    public readonly string Name = name;
    public readonly string Value = value;

    public override string ToString() => Value;
}