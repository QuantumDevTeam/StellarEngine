using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Stellar.Kernel.Data.Collections
{
    public sealed class ConcurrentIdentifierMap<T>
        : ConcurrentDictionary<IIdentifier, T>
    {
        public ConcurrentIdentifierMap(Dictionary<IIdentifier, T> data) : base(data ?? new Dictionary<IIdentifier, T>())
        {
        }
    }
}