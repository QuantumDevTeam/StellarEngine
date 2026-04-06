using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Stellar.Kernel.Data.Collections
{
    /// <summary>
    /// Map which use Identifier as key
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class ConcurrentIdentifierMap<T>
        : ConcurrentDictionary<IIdentifier, T>
    {
        /// <summary>
        /// Initialize  ConcurrentIdentifierMap from base Dictionary
        /// </summary>
        /// <param name="data">Data</param>
        public ConcurrentIdentifierMap(Dictionary<IIdentifier, T> data) : base(data ?? new Dictionary<IIdentifier, T>())
        {
        }
    }
}