using Stellar.Kernel;
using Stellar.Kernel.Quantization;
using Stellar.Core.Quantization;

namespace Stellar.Core.Data.Collections;

public class ConstantTable<T>
    : DataContainer<T>
    where T : IQuant
{
    #region Constructors

    public ConstantTable(MetaQuant meta, Dictionary<IIdentifier, T> data)
        : base(meta, data)
    {
    }

    public ConstantTable(MetaQuant meta)
        : base(meta)
    {
    }

    // TODO: implement DataContainer<T> parametrized constructors

    // public ConstantTable(MetaQuant meta, DataContainer<T> container)
    //     : base(meta, container.Data)
    // {
    // }
    //
    // public ConstantTable(DataContainer<T> container)
    //     : base(container.MetaQuant, container.Data)
    // {
    // }

    #endregion

    #region item support

    public override IQuant? Get(IIdentifier identifier)
    {
        return Data.GetValueOrDefault(identifier);
    }

    #endregion
}