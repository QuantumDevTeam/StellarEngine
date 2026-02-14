using Stellar.Core.Quantization;
using Stellar.Kernel;
using Stellar.Kernel.Registry;
using System.Collections.Concurrent;

namespace Stellar.Core.Data.Registry;

public class MetaQuantsRegistry<T> 
    : IRegistry<T> 
    where T : RegistrableMetaQuant<T>
{
    private static readonly ConcurrentDictionary<IIdentifier, T> Data = new();
    private static readonly Lazy<MetaQuantsRegistry<T>> Registry = new(() => new MetaQuantsRegistry<T>());

    public static IRegistry<T> Instance => Registry.Value;

    public bool Exists(IIdentifier id)
    {
        return Data.ContainsKey(id);
    }

    public bool Register(T obj)
    {
        return Data.TryAdd(obj.Identifier, obj);
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