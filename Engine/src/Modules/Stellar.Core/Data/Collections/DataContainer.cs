using System.Collections;
using Stellar.Kernel;
using Stellar.Kernel.Quantization;
using Stellar.Kernel.Data.Collections;
using Stellar.Kernel.Data.Registry;
using Stellar.Core.Quantization;
using Stellar.Core.Data.Registry;

namespace Stellar.Core.Data.Collections;

public abstract class DataContainer<T>
    : Quant<MetaQuant>, IDataContainer, IEnumerable<T>
    where T : IIdentifiableQuantumObject
{
    public ConcurrentIdentifierMap<IIdentifiableQuantumObject> Data { get; }

    public void Register(IQuantumObject registry)
    {
        if (registry is IRegistry<IDataContainer> containerRegistry)
            containerRegistry.Register(this);
    }

    #region Constructors

    protected DataContainer(MetaQuant meta, ConcurrentIdentifierMap<IIdentifiableQuantumObject> data)
        : base(meta)
    {
        Data = data;
        Register(DataContainerRegistry.Instance);
    }

    protected DataContainer(MetaQuant meta, Dictionary<IIdentifier, T> data)
        : this(meta,
            new ConcurrentIdentifierMap<IIdentifiableQuantumObject>(
                data as Dictionary<IIdentifier, IIdentifiableQuantumObject>))
    {
    }

    protected DataContainer(MetaQuant meta)
        : this(meta, [])
    {
    }

    #endregion

    public static IDataContainer? GetContainer(IIdentifier identifier) =>
        DataContainerRegistry.Instance.Get(identifier);

    #region Item support

    public abstract IIdentifiableQuantumObject? Get(IIdentifier identifier);
    public virtual bool Set(IIdentifiableQuantumObject obj) => false;
    public virtual T? Pop(IIdentifier identifier) => default;

    public T this[IIdentifier key]
    {
        get => (T)Get(key)! ?? throw new KeyNotFoundException();
        set => Set(value);
    }

    #endregion

    #region Encapsulation

    public bool ContainsKey(IIdentifier key) => Data.ContainsKey(key);
    public bool Contains(IIdentifiableQuantumObject obj) => Data.ContainsKey(obj.UID);
    public int Count => Data.Count;
    public bool IsEmpty => Data.IsEmpty;

    public ICollection<IIdentifier> Keys => Data.Keys;
    public ICollection<T> Values => (ICollection<T>)Data.Values.OfType<T>();

    public void Clear() => Data.Clear();

    public IEnumerator<T> GetEnumerator() => Values.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    #endregion

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