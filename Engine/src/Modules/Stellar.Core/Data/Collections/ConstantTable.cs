using Stellar.Kernel;
using Stellar.Kernel.Quantization;
using Stellar.Kernel.Data.Collections;
using Stellar.Core.Quantization;

namespace Stellar.Core.Data.Collections;

public class ConstantTable<T>
    : DataContainer<T>
    where T : IQuant
{
    #region Constructors

    public ConstantTable(MetaQuant meta, ConcurrentIdentifierMap<IQuant> data)
        : base(meta, data)
    {
    }

    public ConstantTable(MetaQuant meta, Dictionary<IIdentifier, T> data)
        : base(meta, data)
    {
    }

    public ConstantTable(MetaQuant meta)
        : base(meta)
    {
    }

    public ConstantTable()
        : base(new MetaQuant())
    {
    }

    #endregion

    #region item support

    public override IQuant? Get(IIdentifier identifier)
    {
        return Data.GetValueOrDefault(identifier);
    }

    #endregion
}