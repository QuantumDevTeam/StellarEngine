namespace Stellar.Core.Data.File;

public sealed record Path(Domain Domain, string Value)
{
    public override string ToString() => $"{Domain}:{Value}";
}