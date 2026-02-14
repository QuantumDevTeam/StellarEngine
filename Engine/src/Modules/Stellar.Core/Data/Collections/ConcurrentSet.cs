using System.Collections.Concurrent;

namespace Stellar.Core.Data.Collections;

public sealed class ConcurrentSet<T> 
    where T : notnull
{
    private readonly ConcurrentDictionary<T, byte> _dictionary = new();
    
    public bool Add(T item) => _dictionary.TryAdd(item, 0);
    public bool Remove(T item) => _dictionary.TryRemove(item, out _);
    public bool Contains(T item) => _dictionary.ContainsKey(item);
    public int Count => _dictionary.Count;
    public void Clear() => _dictionary.Clear();

    public IEnumerator<T> GetEnumerator() => _dictionary.Keys.GetEnumerator();
}