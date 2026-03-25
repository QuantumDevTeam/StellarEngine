using Stellar.Kernel;
using Stellar.Kernel.Quantization;

namespace Stellar.Core.Quantization;

/// <summary>
/// Core engine object == EngineObject = Quant
/// </summary>
/// <param name="meta">MetaQuant with data for Quant</param>
/// <typeparam name="TMeta">Type of MetaQuant</typeparam>
public abstract class Quant<TMeta>(TMeta meta) 
    : IQuant
    where TMeta : IMetaQuant
{
    /// <summary>
    /// clear MetaQuant
    /// </summary>
    public IMetaQuant Meta { get; init; } = meta;
    
    /// <summary>
    /// MetaQuant for this object
    /// </summary>
    public TMeta MetaQuant => (TMeta)Meta;
    
    /// <summary>
    /// Unique ID
    /// </summary>
    public IIdentifier UID => Meta.UID;
    
    /// <summary>
    /// Object Identifier
    /// </summary>
    public Identifier Identifier => Identifier.Get(Meta.UID);
}