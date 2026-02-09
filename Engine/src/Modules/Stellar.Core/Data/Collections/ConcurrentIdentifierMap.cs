using System.Collections.Concurrent;
using Stellar.Kernel.Identification;

namespace Stellar.Core.Data.Collections;

public sealed class ConcurrentIdentifierMap<T>(Dictionary<IIdentifier, T>? data)
    : ConcurrentDictionary<IIdentifier, T>(data ?? new Dictionary<IIdentifier, T>());