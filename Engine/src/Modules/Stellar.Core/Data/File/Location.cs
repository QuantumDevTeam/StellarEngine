using Stellar.Core.Quantization;
using Stellar.Kernel;

namespace Stellar.Core.Data.File;

public sealed class Location(Domain domain, string path, IIdentifier? identifier = null)
    : RegistrableMetaQuant<Location>(identifier)
{
    public readonly Domain Domain = domain;
    public readonly string Path = path;

    public override string ToString() => $"{Domain}://{Path}";
}