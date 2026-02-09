using Stellar.Kernel;
using System.Collections.Concurrent;

namespace Stellar.Core.Data.Collections;

public sealed class ConcurrentIdentifierMap<T>(Dictionary<IIdentifier, T>? data)
    : ConcurrentDictionary<IIdentifier, T>(data ?? new Dictionary<IIdentifier, T>());