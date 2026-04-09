using Stellar.Kernel;
using Stellar.Kernel.Quantization;

namespace Stellar.Core.Quantization;

/// <inheritdoc/>
/// <typeparam name="TMeta">Type of meta. Must inherit on <see cref="Stellar.Core.Quantization.MetaQuant"/></typeparam>
public abstract class Quant<TMeta>
    : IQuant
    where TMeta : IMetaQuant
{
    /// <inheritdoc/>
    public IMetaQuant Meta { get; init; }

    /// <summary>
    /// Meta Quant typed as <typeparamref name="TMeta"/>>
    /// </summary>
    public TMeta MetaQuant => (TMeta)Meta;

    /// <inheritdoc/>
    public IIdentifier UID => Meta.UID;

    /// <summary>
    /// Create Quant
    /// </summary>
    /// <param name="meta">His Meta</param>
    protected Quant(TMeta meta)
    {
        Meta = meta;
    }
}