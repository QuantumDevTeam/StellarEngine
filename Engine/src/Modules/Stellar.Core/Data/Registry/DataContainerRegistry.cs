using System.Collections.Concurrent;
using Stellar.Kernel;
using Stellar.Kernel.Registry;
using Stellar.Core.Data.Collections;

namespace Stellar.Core.Data.Registry;

public class DataContainerRegistry : IRegistry<IDataContainer>
{
    private static readonly ConcurrentDictionary<IIdentifier, IDataContainer> Containers = new();
    private static readonly Lazy<DataContainerRegistry> Registry = new(() => new DataContainerRegistry());

    public static IRegistry<IDataContainer> Instance => Registry.Value;

    public void Register(IDataContainer obj)
    {
        Containers.TryAdd(obj.Id, obj);
    }

    public bool Exists(IIdentifier identifier)
    {
        return Containers.ContainsKey(identifier);
    }

    public IDataContainer? Get(IIdentifier identifier)
    {
        return Containers.GetValueOrDefault(identifier);
    }

    public IDataContainer? Pop(IIdentifier identifier)
    {
        Containers.TryRemove(identifier, out var obj);
        return obj;
    }

    public int Size => Containers.Count;

    public ICollection<IIdentifier> Keys => Containers.Keys;
    public ICollection<IDataContainer> Values => Containers.Values;
}