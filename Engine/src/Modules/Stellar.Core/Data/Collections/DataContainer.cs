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
    protected FastConcurrentIdentifierMap<T> Data { get; }

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
    protected DataContainer(MetaQuant meta, FastConcurrentIdentifierMap<T> data)
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
        : this(meta, new FastConcurrentIdentifierMap<T>(data))
    {
    }

    /// <summary>
    /// Create container by using meta only
    /// </summary>
    /// <param name="meta">A Meta Quant of this container</param>
    protected DataContainer(MetaQuant meta)
        : this(meta, new Dictionary<IIdentifier, T>())
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
    
    /// <summary>
    /// Updates the main snapshot with current dictionary values and clears the hot items list.
    /// </summary>
    /// <remarks>
    /// Call this method once per frame before iteration. The method is thread-safe but acquires a write lock,
    /// so do not call it too frequently.
    /// </remarks>
    public void RefreshSnapshot() => Data.RefreshSnapshot();

    /// <inheritdoc/>
    public bool ContainsKey(IIdentifier key) => Data.ContainsKey(key);

    /// <inheritdoc/>
    public bool Contains(IIdentifiableQuantumObject obj) => Data.ContainsKey(obj.UID);

    /// <inheritdoc/>
    public int Count => Data.Count;

    /// <inheritdoc/>
    public bool IsEmpty => Data.IsEmpty;

    public T[] Values => Data.GetSnapshot();

    /// <summary>
    /// Clear container.
    /// </summary>
    public void Clear() => Data.Clear();
    
    /// <summary>
    /// Executes an action for every element in the container, including hot items.
    /// </summary>
    /// <param name="action">The action to perform on each element.</param>
    /// <remarks>
    /// Iterates first over the snapshot, then over hot items. The iteration is lock‑free and thread‑safe.
    /// </remarks>
    public void ForEachReadOnly(Action<T> action) => Data.ForEachReadOnly(action);
    
    /// <summary>
    /// Iterates over elements until a predicate returns <c>false</c>.
    /// </summary>
    /// <param name="predicate">A function that receives an element and returns <c>true</c> to continue iteration.</param>
    public void ForEachUntil(Func<T, bool> predicate) => Data.ForEachUntil(predicate);

    /// <inheritdoc/>
    public IEnumerator<T> GetEnumerator() => Data.GetEnumerator();

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