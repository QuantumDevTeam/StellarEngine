using Stellar.Core.Quantization;
using Stellar.Kernel.Identification;
using Stellar.Kernel.Registry;

namespace Stellar.Core.Data.Registry;

public class IdentifierRegistry : IRegistry<Identifier>
{
    public void Register(Identifier obj)
    {
        throw new NotImplementedException();
    }

    public bool Exists(IIdentifier identifier)
    {
        throw new NotImplementedException();
    }

    public Identifier? Get(IIdentifier id)
    {
        throw new NotImplementedException();
    }

    public Identifier? Pop(IIdentifier id)
    {
        throw new NotImplementedException();
    }

    public int Size => throw new NotImplementedException();
    
    ICollection<Identifier> Values => throw new NotImplementedException();
}