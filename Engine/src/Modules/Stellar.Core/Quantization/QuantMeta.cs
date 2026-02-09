using Stellar.Kernel.Identification;
using Stellar.Kernel.Quantization;

namespace Stellar.Core.Quantization;

public class QuantMeta(Identifier? identifier = null) : IQuantMeta
{
    public IIdentifier Identifier { get; } = identifier ?? new Identifier();
}