using System.Collections.Concurrent;
using Stellar.Core.Quantization;
using Stellar.Kernel;
using Stellar.Kernel.Registry;

namespace Stellar.Core.Data.Registry;

public class QuantsRegistry<T, TMeta> 
    : IRegistry<T>
    where T : IRegistrableQuantInterface<T, TMeta>
    where TMeta : MetaQuant
{
    private static readonly ConcurrentDictionary<IIdentifier, T> Data = new();
    private static readonly Lazy<QuantsRegistry<T, TMeta>> Registry = new(() => new QuantsRegistry<T, TMeta>());

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