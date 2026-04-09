using Stellar.Kernel;
using Stellar.Kernel.Quantization;
using Stellar.Kernel.Data.Collections;
using Stellar.Core.Quantization;

namespace Stellar.Core.Data.Collections;

/// <summary>
/// Represents a quantum object that can contain and manage other identifiable quantum objects.
/// </summary>
/// <inheritdoc/>
public class WritableTable<T>
    : ConstantTable<T>
    where T : IIdentifiableQuantumObject
{
    #region Constructors

    /// <inheritdoc/>
    public WritableTable(MetaQuant meta, ConcurrentIdentifierMap<T> data)
        : base(meta, data)
    {
    }

    /// <inheritdoc/>
    public WritableTable(MetaQuant meta, Dictionary<IIdentifier, T> data)
        : base(meta, data)
    {
    }

    /// <inheritdoc/>
    public WritableTable(MetaQuant meta)
        : base(meta)
    {
    }

    /// <inheritdoc/>
    public WritableTable()
    {
    }

    #endregion

    #region item support

    /// <inheritdoc/>
    public override bool Set(IIdentifiableQuantumObject obj)
    {
        return obj is T typedObj && Data.TryAdd(obj.UID, typedObj);
    }

    /// <inheritdoc/>
    public override T? Pop(IIdentifier id)
    {
        Data.TryRemove(id, out var quant);
        return quant;
    }

    #endregion
}