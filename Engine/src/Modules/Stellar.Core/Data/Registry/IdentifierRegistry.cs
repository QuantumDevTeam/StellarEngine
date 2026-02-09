using System.Collections.Concurrent;
using Stellar.Kernel.Identification;
using Stellar.Kernel.Registry;

namespace Stellar.Core.Data.Registry;

public class IdentifierRegistry : IRegistry<Identifier>
{
    private static readonly ConcurrentDictionary<Guid, Identifier> Identifiers = new();
    private static readonly Lazy<IdentifierRegistry> Registry = new(() => new IdentifierRegistry());

    public static IRegistry<Identifier> Instance => Registry.Value;

    public void Register(Identifier obj)
    {
        Identifiers.TryAdd(obj.UID, obj);
    }

    public bool Exists(IIdentifier identifier)
    {
        return Identifiers.ContainsKey(identifier.UID);
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