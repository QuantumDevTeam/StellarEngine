using System.Collections.Concurrent;
using Stellar.Kernel;
using Stellar.Kernel.Data.Registry;

namespace Stellar.Core.Data.Registry;

public class IdentifierRegistry
    : IRegistry<Identifier>
{
    private static readonly ConcurrentDictionary<Guid, Identifier> Identifiers = new();
    
    private static readonly Lazy<IdentifierRegistry> Registry = new(() => new IdentifierRegistry());
    public static IdentifierRegistry Instance => Registry.Value;

    public bool Exists(IIdentifier id)
    {
        return Identifiers.ContainsKey(id.UID);
    }

    public bool Register(Identifier obj)
    {
        return Identifiers.TryAdd(obj.UID, obj);
    }

    public Identifier? Get(Guid uid)
    {
        return Identifiers.GetValueOrDefault(uid);
    }

    public Identifier? Get(IIdentifier id)
    {
        return Get(id.UID);
    }

    public Identifier? Pop(Guid uid)
    {
        Identifiers.TryRemove(uid, out var obj);
        return obj;
    }

    public Identifier? Pop(IIdentifier id)
    {
        return Pop(id.UID);
    }

    public int Size => Identifiers.Count;
    public ICollection<IIdentifier> Keys => (ICollection<IIdentifier>)Identifiers.Values;
    public ICollection<Identifier> Values => Identifiers.Values;
}