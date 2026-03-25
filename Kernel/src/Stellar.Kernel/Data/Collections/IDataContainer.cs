using Stellar.Kernel.Quantization;

namespace Stellar.Kernel.Data.Collections
{
    public interface IDataContainer
        : IRegistrableQuant
    {
        ConcurrentIdentifierMap<IQuant> Data { get; }

#if NETSTANDARD2_0
        IQuant Get(IIdentifier id);
#else
#nullable enable
        IQuant? Get(IIdentifier id);
#endif
        bool Contains(IIdentifier key);

        int Count { get; }
        bool IsEmpty { get; }
    }
}