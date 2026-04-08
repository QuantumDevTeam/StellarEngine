using System.Collections.Concurrent;
using Stellar.Kernel;
using Stellar.Kernel.Data.Collections;
using Stellar.Kernel.Data.Registry;

namespace Stellar.Core.Data.Registry;

public class DataContainerRegistry
    : IRegistry<IDataContainer>
{
    private static readonly ConcurrentDictionary<IIdentifier, IDataContainer> Containers = new();

    private static readonly Lazy<DataContainerRegistry> Registry = new(() => new DataContainerRegistry());
    public static DataContainerRegistry Instance => Registry.Value;

    public bool Exists(IIdentifier id)
    {
        return Containers.ContainsKey(id);
    }

    public bool Register(IDataContainer obj)
    {
        return Containers.TryAdd(obj.UID, obj);
    }

    public IDataContainer? Get(IIdentifier id)
    {
        return Containers.GetValueOrDefault(id);
    }

    public IDataContainer? Pop(IIdentifier id)
    {
        Containers.TryRemove(id, out var obj);
        return obj;
    }

    public int Size => Containers.Count;

    public ICollection<IIdentifier> Keys => Containers.Keys;
    public ICollection<IDataContainer> Values => Containers.Values;
}