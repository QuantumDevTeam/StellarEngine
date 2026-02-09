using Stellar.Kernel;

namespace Stellar.Core.Data.File;

public sealed record Path(IIdentifier Domain, string Value)
{
    public override string ToString() => $"{Domain}:{Value}";
}