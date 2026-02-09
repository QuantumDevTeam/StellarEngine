using Stellar.Kernel.Identification;

namespace Stellar.Kernel.Registry
{
    public interface IRegistry<T> where T : class
    {
        void Register(T obj);
        bool Exists(IIdentifier identifier);
#if NETSTANDARD2_0
        T Get(IIdentifier id);
        T Pop(IIdentifier id);
#else
        T? Get(IIdentifier id);
        T? Pop(IIdentifier id);
#endif
    }
}