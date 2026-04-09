using System.Collections;
using Stellar.Kernel;
using Stellar.Kernel.Quantization;
using Stellar.Kernel.Data.Collections;
using Stellar.Kernel.Data.Registry;
using Stellar.Core.Quantization;
using Stellar.Core.Data.Registry;

namespace Stellar.Core.Data.Collections;

/// <inheritdoc cref="IDataContainer" />
/// <typeparam name="T">Type of stored data</typeparam>
public abstract class DataContainer<T>
    : Quant<MetaQuant>, IDataContainer, IEnumerable<T>
    where T : IIdentifiableQuantumObject
{
    protected ConcurrentIdentifierMap<T> Data { get; }

    /// <inheritdoc/>
    public void Register(IQuantumObject? registry = null)
    {
        registry ??= DataContainerRegistry.Instance;
        if (registry is IRegistry<IDataContainer> identifierRegistry)
            identifierRegistry.Register(this);
    }

    #region Constructors

    /// <summary>
    /// Create container by using meta and typed data
    /// </summary>
    /// <param name="meta">A Meta Quant of this container</param>
    /// <param name="data">An initial data for this container</param>
    protected DataContainer(MetaQuant meta, ConcurrentIdentifierMap<T> data)
        : base(meta)
    {
        Data = data;
        Register(DataContainerRegistry.Instance);
    }

    /// <summary>
    /// Create container by using meta and data stored in a Dictionary
    /// </summary>
    /// <param name="meta">A Meta Quant of this container</param>
    /// <param name="data">An initial data stored in a Dictionary</param>
    protected DataContainer(MetaQuant meta, Dictionary<IIdentifier, T> data)
        : this(meta, new ConcurrentIdentifierMap<T>(data))
    {
    }

    /// <summary>
    /// Create container by using meta only
    /// </summary>
    /// <param name="meta">A Meta Quant of this container</param>
    protected DataContainer(MetaQuant meta)
        : this(meta, [])
    {
    }

    #endregion

    /// <summary>
    /// Gets container by his Identifier
    /// </summary>
    /// <param name="identifier"></param>
    /// <returns></returns>
    public static IDataContainer? GetContainer(IIdentifier identifier) =>
        DataContainerRegistry.Instance.Get(identifier);

    #region Item support

    /// <inheritdoc/>
    public abstract IIdentifiableQuantumObject? Get(IIdentifier identifier);

    /// <summary>
    /// Store quantum object by its identifier.
    /// </summary>
    /// <param name="obj">The object to store.</param>
    /// <returns><c>true</c> if object was successfully stored; otherwise, <c>false</c>.</returns>
    public virtual bool Set(IIdentifiableQuantumObject obj) => false;
    
    /// <summary>
    /// Attempts to remove and return the object stored in container.
    /// </summary>
    /// <param name="id">The unique identifier of the object.</param>
    /// <returns>The object if found; otherwise, <c>null</c>.</returns>
    public virtual T? Pop(IIdentifier id) => default;

    /// <summary>
    /// Base item operations with container
    /// </summary>
    /// <param name="key">The unique identifier of the object.</param>
    /// <exception cref="KeyNotFoundException">Thrown if <paramref name="key"/> is not found in container.</exception>
    public T this[IIdentifier key]
    {
        get => (T)Get(key)! ?? throw new KeyNotFoundException();
        set => Set(value);
    }

    #endregion

    #region Encapsulation

    /// <inheritdoc/>
    public bool ContainsKey(IIdentifier key) => Data.ContainsKey(key);
    
    /// <inheritdoc/>
    public bool Contains(IIdentifiableQuantumObject obj) => Data.ContainsKey(obj.UID);
    
    /// <inheritdoc/>
    public int Count => Data.Count;
    
    /// <inheritdoc/>
    public bool IsEmpty => Data.IsEmpty;

    public ICollection<IIdentifier> Keys => Data.Keys;
    public ICollection<T> Values => Data.Values;

    /// <summary>
    /// Clear container.
    /// </summary>
    public void Clear() => Data.Clear();

    /// <inheritdoc/>
    public IEnumerator<T> GetEnumerator() => Values.GetEnumerator();
    
    /// <inheritdoc/>
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    #endregion

    /// <inheritdoc/>
    public void Unregister(IQuantumObject? registry = null)
    {
        registry ??= DataContainerRegistry.Instance;
        if (registry is IRegistry<IDataContainer> identifierRegistry)
            identifierRegistry.Pop(UID);
    }

    /// <inheritdoc/>
    public void Dispose() => Unregister();
}