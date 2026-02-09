using Stellar.Kernel.Identification;

namespace Stellar.Core.Quantization;

public class Identifier : IIdentifier, IDisposable
{
    public Guid UID { get; }

    private Identifier(Guid uid)
    {
        UID = uid;
    }

    public static implicit operator Identifier(Guid uid) => new(uid);
    public static implicit operator Identifier(string uid) => new(new Guid(uid));
    public static implicit operator Identifier(byte[] uid) => new(new Guid(uid));

    public static Identifier Get<T>(T data)
    {
        
    }

    public override string ToString() => $"UID#{UID}";
    public override int GetHashCode() => UID.GetHashCode();

    public void Dispose()
    {
    }
}