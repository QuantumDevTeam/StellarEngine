using Stellar.Kernel;
using Stellar.Kernel.Quantization;

namespace Stellar.Core.Quantization;

public abstract class Quant<T>(T meta) : IQuant where T : MetaQuant
{
    public IMetaQuant Meta { get; init; } = meta;
    public T MetaQuant => (T)Meta;
    public IIdentifier Id => Meta.Identifier;
    public Identifier Identifier => Identifier.Get(Meta.Identifier);
}