using Stellar.Kernel;
using Stellar.Kernel.Quantization;

namespace Stellar.Core.Data.Collections;

public interface IDataContainer : IQuant
{
    ConcurrentIdentifierMap<IQuant> Data { get; init; }

    IQuant? Get(IIdentifier key);
    bool Contains(IIdentifier key);

    int Count { get; }
    bool IsEmpty { get; }
}