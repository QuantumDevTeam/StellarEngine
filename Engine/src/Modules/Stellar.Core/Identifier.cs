using Stellar.Kernel;
using Stellar.Kernel.Quantization;
using Stellar.Kernel.Data.Registry;
using Stellar.Core.Data.Registry;

namespace Stellar.Core;

public class Identifier
    : IIdentifier
{
    public Guid UID { get; }
    protected string _techName { get; }

    public void Register(IQuantumObject registry)
    {
        if (registry is IRegistry<IIdentifier> identifierRegistry)
            identifierRegistry.Register(this);
    }

    public void Register()
    {
        Register(IdentifierRegistry.Instance);
    }

    #region Constuctors

    protected Identifier(Guid uid)
    {
        UID = uid;
        _techName = $"Identifier#{UID}";
        Register();
    }

    public Identifier()
        : this(Guid.NewGuid())
    {
    }

    #endregion

    #region Get

    public static Identifier Get(Guid data) =>
        IdentifierRegistry.Instance.Get(data) ?? new Identifier(Guid.NewGuid());

    public static Identifier Get(byte[] data) => Get(new Guid(data));

    public static Identifier Get(string data) => Get(new Guid(data));

    public static Identifier Get(IIdentifier data) => Get(data.UID);

    #endregion

    #region implict operator

    public static implicit operator Identifier(Guid uid) => Get(uid);
    public static implicit operator Identifier(string uid) => Get(uid);
    public static implicit operator Identifier(byte[] uid) => Get(uid);

    #endregion

    public static bool Exist(Guid uid) => IdentifierRegistry.Instance.Get(uid) != null;

    public override string ToString() => _techName;
    public override int GetHashCode() => UID.GetHashCode();

    public void Unregister(IQuantumObject registry)
    {
        if (registry is IRegistry<IIdentifier> identifierRegistry)
            identifierRegistry.Pop(this);
    }

    public void Dispose()
    {
        Unregister(IdentifierRegistry.Instance);
    }
}