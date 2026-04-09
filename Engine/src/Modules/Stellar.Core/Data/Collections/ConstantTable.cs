using Stellar.Kernel;
using Stellar.Kernel.Quantization;
using Stellar.Kernel.Data.Collections;
using Stellar.Core.Quantization;

namespace Stellar.Core.Data.Collections;

/// <summary>
/// Represents a quantum object that can only contain other identifiable quantum objects.
/// </summary>
/// <inheritdoc/>
public class ConstantTable<T>
    : DataContainer<T>
    where T : IIdentifiableQuantumObject
{
    #region Constructors

    /// <inheritdoc/>
    public ConstantTable(MetaQuant meta, FastConcurrentIdentifierMap<T> data)
        : base(meta, data)
    {
    }

    /// <inheritdoc/>
    public ConstantTable(MetaQuant meta, Dictionary<IIdentifier, T> data)
        : base(meta, data)
    {
    }

    /// <inheritdoc/>
    public ConstantTable(MetaQuant meta)
        : base(meta)
    {
    }

    /// <inheritdoc/>
    public ConstantTable()
        : base(new MetaQuant(Identifier.CreateAndRegister()))
    {
    }

    #endregion

    #region item support

    /// <inheritdoc/>
    public override IIdentifiableQuantumObject? Get(IIdentifier identifier)
    {
        return Data.GetValueOrDefault(identifier);
    }

    #endregion
}