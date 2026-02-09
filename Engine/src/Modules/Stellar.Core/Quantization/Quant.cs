using Stellar.Kernel.Identification;
using Stellar.Kernel.Quantization;

namespace Stellar.Core.Quantization;

public abstract class Quant<T>(T meta) : IQuant where T : QuantMeta
{
    public IQuantMeta Meta { get; init; } = meta;
    public T MetaData => (T)Meta;
    public IIdentifier Id => Meta.Identifier;
    public Identifier Identifier => Identifier.Get(Meta.Identifier);
}