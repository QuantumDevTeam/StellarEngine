using Stellar.Kernel;
using Stellar.Kernel.Quantization;
using Stellar.Core.Data.Registry;

namespace Stellar.Core;

public class Identifier : IIdentifier, IRegistrableQuantumObject
{
    public Guid UID { get; init; }
    protected string _techName { get; init; }

    public void Register()
    {
        IdentifierRegistry.Instance.Register(this);
    }

    #region Constuctors

    protected Identifier(Guid uid)
    {
        UID = uid;
        _techName = $"UID#{UID}";
        Register();
    }

    public Identifier() : this(Guid.NewGuid())
    {
    }

    #endregion
    
    #region Get

    public static Identifier Get(Guid data) => ((IdentifierRegistry)IdentifierRegistry.Instance).Get(data)
                                               ?? new Identifier(Guid.NewGuid());

    public static Identifier Get(byte[] data) => Get(new Guid(data));

    public static Identifier Get(string data) => Get(new Guid(data));

    public static Identifier Get(IIdentifier data)
    {
        if (data is Identifier identifier) return identifier;
        return Get(data.UID);
    }

    #endregion

    #region implict operator

    public static implicit operator Identifier(Guid uid) => Get(uid);
    public static implicit operator Identifier(string uid) => Get(uid);
    public static implicit operator Identifier(byte[] uid) => Get(uid);

    #endregion
    
    public static bool Exist(Guid uid) => ((IdentifierRegistry)IdentifierRegistry.Instance).Get(uid) != null;

    public override string ToString() => _techName;
    public override int GetHashCode() => UID.GetHashCode();

    public void Unregister()
    {
        IdentifierRegistry.Instance.Pop(this);
    }

    public void Dispose()
    {
        Unregister();
    }
}