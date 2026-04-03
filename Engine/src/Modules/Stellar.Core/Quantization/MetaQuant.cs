using Stellar.Kernel;
using Stellar.Kernel.Quantization;

namespace Stellar.Core.Quantization;

/// <summary>
/// MetaQuant with data for Quant
/// </summary>
/// <param name="identifier">Quant identifier</param>
public class MetaQuant(IIdentifier? identifier = null)
    : IMetaQuant
{
    /// <summary>
    /// Unique ID
    /// </summary>
    public IIdentifier UID { get; } = identifier ?? new Identifier();
    
    public override int GetHashCode() => UID.GetHashCode();
}