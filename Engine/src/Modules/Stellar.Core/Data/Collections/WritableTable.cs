using Stellar.Kernel;
using Stellar.Kernel.Quantization;
using Stellar.Kernel.Data.Collections;
using Stellar.Core.Quantization;

namespace Stellar.Core.Data.Collections;

public class WritableTable<T>
    : ConstantTable<T>
    where T : IQuant
{
    #region Constructors

    public WritableTable(MetaQuant meta, ConcurrentIdentifierMap<IQuant> data)
        : base(meta, data)
    {
    }

    public WritableTable(MetaQuant meta, Dictionary<IIdentifier, T> data)
        : base(meta, data)
    {
    }

    public WritableTable(MetaQuant meta)
        : base(meta)
    {
    }

    public WritableTable()
    {
    }

    #endregion

    #region item support

    public override bool Set(IQuant quant)
    {
        return Data.TryAdd(quant.UID, quant);
    }

    public override T? Pop(IIdentifier identifier)
    {
        Data.TryRemove(identifier, out var quant);
        return (T?)quant;
    }

    #endregion
}