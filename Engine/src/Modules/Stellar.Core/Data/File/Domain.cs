using Stellar.Core.Quantization;

namespace Stellar.Core.Data.File;

public sealed class Domain : MetaQuant, IDisposable
{
    public readonly DomainType Type;
    public readonly string Value;

    public Domain(DomainType type, string value)
    {
        Type = type;
        Value = value;
        throw new NotImplementedException();
    }

    public override string ToString() => Value;

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}