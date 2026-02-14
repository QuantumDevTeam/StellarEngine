using Stellar.Kernel;
using Stellar.Kernel.Quantization;

namespace Stellar.Core.Quantization;

public abstract class Quant<TMeta>(TMeta meta) 
    : IQuant 
    where TMeta : MetaQuant
{
    public IMetaQuant Meta { get; init; } = meta;
    public TMeta MetaQuant => (TMeta)Meta;
    public IIdentifier Id => Meta.Identifier;
    public Identifier Identifier => Identifier.Get(Meta.Identifier);
}