using Stellar.Kernel;
using Stellar.Kernel.Quantization;
using Stellar.Core.Quantization;
using Stellar.Core.Data.Registry;

namespace Stellar.Core.Data.Collections;

public abstract class DataContainer<T> : Quant<MetaQuant>, IDataContainer, IDisposable
{
    public ConcurrentIdentifierMap<IQuant> Data { get; init; }

    #region Constructors

    protected DataContainer(MetaQuant meta, Dictionary<IIdentifier, T> data)
        : base(meta)
    {
        Data = new ConcurrentIdentifierMap<IQuant>(data as Dictionary<IIdentifier, IQuant>);
        DataContainerRegistry.Instance.Register(this);
    }

    protected DataContainer(MetaQuant data)
        : this(data, new Dictionary<IIdentifier, T>())
    {
    }

    #endregion

    public static IDataContainer? GetContainer(IIdentifier identifier) =>
        DataContainerRegistry.Instance.Get(identifier);

    public IQuant? Get(IIdentifier key)
    {
        return Data.GetValueOrDefault(key);
    }

    public T this[IIdentifier key] => (T)Get(key)! ?? throw new KeyNotFoundException();

    public bool Contains(IIdentifier key) => Data.ContainsKey(key);
    public int Count => Data.Count;
    public bool IsEmpty => Data.IsEmpty;

    public ICollection<IIdentifier> Keys => Data.Keys;
    public ICollection<IQuant> Values => Data.Values;

    public void Clear() => Data.Clear();

    public void Dispose()
    {
        DataContainerRegistry.Instance.Pop(Id);
    }
}