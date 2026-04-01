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
        : base(meta)
    {
    }

    public WritableTable(MetaQuant meta)
        : base(meta, new Dictionary<IIdentifier, T>())
    {
    }
    
    public WritableTable(MetaQuant meta, DataContainer<T> container)
        : base(meta, container.Data)
    {
    }
    
    public WritableTable(DataContainer<T> container)
        : base(container.MetaQuant, container.Data)
    {
    }


    #endregion
    
    #region item support
    
    public override bool Set(IQuant obj)
    {
        return Data.TryAdd(quant.UID, obj);
    }
    
    public override T? Pop(IIdentifier identifier)
    {
        if (Data.TryRemove(identifier, our var obj)) return (T)obj;
    }
    
    #endregion
}
