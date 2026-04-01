using Stellar.Kernel;
using Stellar.Kernel.Quantization;
using Stellar.Core.Quantization;

namespace Stellar.Core.Data.Collections;

public class WritableTable<T>
    : ConstantTable<T>
    where T : IQuant
{
    #region Constructors

    public WritableTable(MetaQuant meta, Dictionary<IIdentifier, T> data)
        : base(meta, data)
    {
    }

    public WritableTable(MetaQuant meta)
        : base(meta)
    {
    }
    
    // TODO: implement DataContainer<T> parametrized constructors
    
    // public WritableTable(MetaQuant meta, DataContainer<T> container)
    //     : base(meta, container.Data)
    // {
    // }
    //
    // public WritableTable(DataContainer<T> container)
    //     : base(container.MetaQuant, container.Data)
    // {
    // }


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
