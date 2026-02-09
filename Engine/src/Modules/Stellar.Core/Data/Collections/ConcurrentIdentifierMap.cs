using System.Collections.Concurrent;
using Stellar.Core.Quantization;

namespace Stellar.Core.Data.Collections;

public sealed class ConcurrentIdentifierMap<T>(Dictionary<Identifier, T>? data)
    : ConcurrentDictionary<Identifier, T>(data ?? new Dictionary<Identifier, T>());