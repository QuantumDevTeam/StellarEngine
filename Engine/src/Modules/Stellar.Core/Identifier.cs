using Stellar.Kernel;
using Stellar.Core.Data.Registry;

namespace Stellar.Core;

public class Identifier : IIdentifier, IDisposable
{
    public Guid UID { get; }

    private Identifier(Guid uid)
    {
        UID = uid;
        IdentifierRegistry.Instance.Register(this);
    }

    public Identifier() : this(Guid.NewGuid())
    {
    }

    public static Identifier Get(Guid data) => ((IdentifierRegistry)IdentifierRegistry.Instance).Get(data)
                                               ?? new Identifier(Guid.NewGuid());

    public static Identifier Get(string data) => Get(new Guid(data));

    public static Identifier Get(byte[] data) => Get(new Guid(data));

    public static Identifier Get(IIdentifier data)
    {
        if (data is Identifier identifier) return identifier;
        return Get(data.UID);
    }

    public static implicit operator Identifier(Guid uid) => Get(uid);
    public static implicit operator Identifier(string uid) => Get(uid);
    public static implicit operator Identifier(byte[] uid) => Get(uid);

    public override string ToString() => $"UID#{UID}";
    public override int GetHashCode() => UID.GetHashCode();

    public void Dispose()
    {
        IdentifierRegistry.Instance.Pop(this);
    }
}