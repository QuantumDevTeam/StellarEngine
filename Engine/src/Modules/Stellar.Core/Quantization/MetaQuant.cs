using Stellar.Kernel;
using Stellar.Kernel.Quantization;

namespace Stellar.Core.Quantization;

public class MetaQuant(IIdentifier? identifier = null)
    : IMetaQuant
{
    public IIdentifier UID { get; } = identifier ?? new Identifier();
}