namespace Stellar.Kernel.Identification
{
    public interface IRegistry
    {
        bool Exists(IIdentifier identifier);
        object Get(IIdentifier identifier);
    }
}