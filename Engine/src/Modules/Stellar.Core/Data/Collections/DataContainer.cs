using System.Collections;
using Stellar.Kernel;
using Stellar.Kernel.Quantization;
using Stellar.Kernel.Data.Collections;
using Stellar.Core.Quantization;
using Stellar.Core.Data.Registry;
using Stellar.Kernel.Data.Registry;

namespace Stellar.Core.Data.Collections;

public abstract class DataContainer<T>
    : Quant<MetaQuant>, IDataContainer, IEnumerable<T>
{
    public ConcurrentIdentifierMap<IQuant> Data { get; }

    public void Register(IQuantumObject registry)
    {
        if (registry is IRegistry<IDataContainer> containerRegistry)
            containerRegistry.Register(this);
    }

    #region Constructors

    protected DataContainer(MetaQuant meta, Dictionary<IIdentifier, T> data)
        : base(meta)
    {
        Data = new ConcurrentIdentifierMap<IQuant>(data as Dictionary<IIdentifier, IQuant>);
        Register(DataContainerRegistry.Instance);
    }

    protected DataContainer(MetaQuant meta)
        : this(meta, new Dictionary<IIdentifier, T>())
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
    public ICollection<T> Values => (ICollection<T>)Data.Values.OfType<T>();

    public void Clear() => Data.Clear();

    public IEnumerator<T> GetEnumerator() => Values.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public void Unregister(IQuantumObject registry)
    {
        if (registry is IRegistry<IDataContainer> containerRegistry)
            containerRegistry.Pop(UID);
    }

    public void Dispose()
    {
        Unregister(DataContainerRegistry.Instance);
    }
}