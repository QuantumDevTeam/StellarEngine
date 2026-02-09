using System.Collections.Concurrent;
using Stellar.Kernel.Identification;
using Stellar.Kernel.Registry;
using Stellar.Core.Quantization;

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

    public Identifier? Get(IIdentifier id)
    {
        return Identifiers.GetValueOrDefault(id.UID);
    }

    public Identifier? Pop(IIdentifier id)
    {
        Identifiers.TryRemove(id.UID, out var obj);
        return obj;
    }

    public int Size => Identifiers.Count;
    public ICollection<Identifier> Values => Identifiers.Values;
}