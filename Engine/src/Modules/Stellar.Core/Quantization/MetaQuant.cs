using Stellar.Kernel;
using Stellar.Kernel.Quantization;

namespace Stellar.Core.Quantization;

/// <inheritdoc/>
public class MetaQuant
    : IMetaQuant
{
    /// <inheritdoc/>
    public IIdentifier UID { get; }
    
    /// <summary>
    /// Create Meta Quant
    /// </summary>
    /// <param name="identifier">Unique ID</param>
    public MetaQuant(IIdentifier identifier)
    {
        UID = identifier;
    }
    
    /// <inheritdoc/>
    public override int GetHashCode() => UID.GetHashCode();
}