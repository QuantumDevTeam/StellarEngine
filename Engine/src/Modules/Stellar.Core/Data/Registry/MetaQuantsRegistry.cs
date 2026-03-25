using System.Collections.Concurrent;
using Stellar.Kernel;
using Stellar.Kernel.Data.Registry;
using Stellar.Kernel.Quantization;

namespace Stellar.Core.Data.Registry;

public class MetaQuantsRegistry<TMeta>
    : IRegistry<TMeta>
    where TMeta : IRegistrableMetaQuant
{
    private static readonly ConcurrentDictionary<IIdentifier, TMeta> Data = new();
    private static readonly Lazy<MetaQuantsRegistry<TMeta>> Registry = new(() => new MetaQuantsRegistry<TMeta>());

    public static IRegistry<TMeta> Instance => Registry.Value;

    public bool Exists(IIdentifier id)
    {
        return Data.ContainsKey(id);
    }

    public bool Register(TMeta obj)
    {
        return Data.TryAdd(obj.UID, obj);
    }

    public TMeta? Get(IIdentifier id)
    {
        return Data.GetValueOrDefault(id);
    }

    public TMeta? Pop(IIdentifier id)
    {
        Data.TryRemove(id, out var obj);
        return obj;
    }

    public int Size => Data.Count;
    public ICollection<IIdentifier> Keys => Data.Keys;
    public ICollection<TMeta> Values => Data.Values;
}