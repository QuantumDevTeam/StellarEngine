using Stellar.Kernel.Quantization;

namespace Stellar.Kernel.Data.Collections
{
    public interface IDataContainer
        : IRegistrableQuant
    {
        ConcurrentIdentifierMap<IIdentifiableQuantumObject> Data { get; }

#if NETSTANDARD2_0
        IIdentifiableQuantumObject Get(IIdentifier id);
#else
#nullable enable
        IIdentifiableQuantumObject? Get(IIdentifier id);
#endif
        bool ContainsKey(IIdentifier key);
        bool Contains(IIdentifiableQuantumObject obj);

        int Count { get; }
        bool IsEmpty { get; }
    }
}