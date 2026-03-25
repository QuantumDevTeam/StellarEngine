using System.Collections.Concurrent;
using Stellar.Kernel;
using Stellar.Kernel.Data.Registry;
using Stellar.Kernel.Quantization;

namespace Stellar.Core.Data.Registry;

public class QuantsRegistry<T> 
    : IRegistry<T>
    where T : IRegistrableQuant
{
    private static readonly ConcurrentDictionary<IIdentifier, T> Data = new();
    private static readonly Lazy<QuantsRegistry<T>> Registry = new(() => new QuantsRegistry<T>());

    public static IRegistry<T> Instance => Registry.Value;

    public bool Exists(IIdentifier id)
    {
        return Data.ContainsKey(id);
    }

    public bool Register(T obj)
    {
        return Data.TryAdd(obj.UID, obj);
    }

    public T? Get(IIdentifier id)
    {
        return Data.GetValueOrDefault(id);
    }

    public T? Pop(IIdentifier id)
    {
        Data.TryRemove(id, out var obj);
        return obj;
    }

    public int Size => Data.Count;
    public ICollection<IIdentifier> Keys => Data.Keys;
    public ICollection<T> Values => Data.Values;
}