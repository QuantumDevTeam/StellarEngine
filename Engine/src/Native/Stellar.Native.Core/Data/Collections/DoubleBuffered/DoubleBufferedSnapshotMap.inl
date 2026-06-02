#pragma once

template <CHashableKey TKey, typename TValue>
DoubleBufferedSnapshotMap<TKey, TValue>::DoubleBufferedSnapshotMap()
    : _currentSnapshot(std::make_shared<DataSnapshot>()),
      _writeBuffer(std::make_shared<DataSnapshot>())
{
}

template <CHashableKey TKey, typename TValue>
bool DoubleBufferedSnapshotMap<TKey, TValue>::TryAdd(const TKey& key, const TValue& value, bool immediate) const
{
    if (immediate)
    {
        std::lock_guard hotLock(_hotMutex);
        _hotItems[key] = value;
    }
    std::unique_lock lock(_writeMutex);
    _writeBuffer->map[key] = value;
    return true;
}

template <CHashableKey TKey, typename TValue>
std::optional<TValue> DoubleBufferedSnapshotMap<TKey, TValue>::TryGet(const TKey& key, bool immediate) const
{
    if (immediate)
    {
        std::lock_guard hotLock(_hotMutex);
        auto it = _hotItems.find(key);
        if (it != _hotItems.end())
            return it->second;
    }
    auto snapshot = GetSnapshot();
    auto it = snapshot->map.find(key);
    if (it != snapshot->map.end())
        return it->second;
    return std::nullopt;
}

template <CHashableKey TKey, typename TValue>
bool DoubleBufferedSnapshotMap<TKey, TValue>::TryRemove(const TKey& key, TValue& outValue, bool immediate) const
{
    std::unique_lock lock(_writeMutex);
    auto it = _writeBuffer->map.find(key);
    if (it == _writeBuffer->map.end()) return false;
    outValue = it->second;
    _writeBuffer->map.erase(it);
    if (immediate)
    {
        std::lock_guard hotLock(_hotMutex);
        _hotItems.insert[key] = std::nullopt;
    }
    return true;
}

template <CHashableKey TKey, typename TValue>
bool DoubleBufferedSnapshotMap<TKey, TValue>::Contains(const TKey& key, bool immediate) const
{
    if (immediate)
    {
        std::lock_guard hotLock(_hotMutex);
        return _hotItems.contains(key);
    }
    auto snapshot = GetSnapshot();
    return snapshot->map.contains(key);
}

template <CHashableKey TKey, typename TValue>
size_t DoubleBufferedSnapshotMap<TKey, TValue>::size(bool immediate) const
{
    auto snapshot = GetSnapshot();
    size_t size = snapshot->map.size();
    if (immediate)
    {
        std::lock_guard hotLock(_hotMutex);
        size += _hotItems.size();
    }
    return size;
}

template <CHashableKey TKey, typename TValue>
std::generator<const TKey&> DoubleBufferedSnapshotMap<TKey, TValue>::Keys(bool immediate) const
{
    auto snapshot = GetSnapshot();
    std::vector<TKey> hotItems;
    if (immediate)
    {
        std::lock_guard hotLock(_hotMutex);
        hotItems.reserve(_hotItems.size());
        for (const auto& [key, _] : _hotItems)
            hotItems.push_back(key);
    }
    for (const auto& key : hotItems)
        co_yield key;
    for (const auto& [key, _] : snapshot->map)
        co_yield key;
}

template <CHashableKey TKey, typename TValue>
std::generator<const TValue&> DoubleBufferedSnapshotMap<TKey, TValue>::Values(bool immediate) const
{
    auto snapshot = GetSnapshot();
    std::vector<TValue> hotItems;
    if (immediate)
    {
        std::lock_guard hotLock(_hotMutex);
        hotItems.reserve(_hotItems.size());
        for (const auto& [_, value] : _hotItems)
            hotItems.push_back(value);
    }
    for (const auto& value : hotItems)
        co_yield value;
    for (const auto& [_, value] : snapshot->map)
        co_yield value;
}

template <CHashableKey TKey, typename TValue>
void DoubleBufferedSnapshotMap<TKey, TValue>::SwapBuffers()
{
    std::unique_lock lock(_writeMutex);
    {
        std::lock_guard hotLock(_hotMutex);
        for (const auto& [key, value] : _hotItems)
        {
            if (value == std::nullopt)
            {
                _writeBuffer->map.erase(key);
            }
            else
            {
                _writeBuffer->map[key] = value;
            }
        }
    }
    auto newSnapshot = std::make_shared<DataSnapshot>(std::move(*_writeBuffer));
    newSnapshot->version = _currentSnapshot.load(std::memory_order_relaxed)->version + 1;
    _currentSnapshot.store(newSnapshot, std::memory_order_release);
    _writeBuffer = std::make_shared<DataSnapshot>();
}

template <CHashableKey TKey, typename TValue>
bool DoubleBufferedSnapshotMap<TKey, TValue>::TryAdd(const TKey& key, const TValue& value)
{
    return TryAdd(key, value, false);
}

template <CHashableKey TKey, typename TValue>
std::optional<TValue> DoubleBufferedSnapshotMap<TKey, TValue>::TryGet(const TKey& key) const
{
    return TryGet(key, false);
}

template <CHashableKey TKey, typename TValue>
bool DoubleBufferedSnapshotMap<TKey, TValue>::TryRemove(const TKey& key, TValue& outValue)
{
    return TryRemove(key, outValue, false);
}

template <CHashableKey TKey, typename TValue>
bool DoubleBufferedSnapshotMap<TKey, TValue>::Contains(const TKey& key) const
{
    return Contains(key, false);
}

template <CHashableKey TKey, typename TValue>
size_t DoubleBufferedSnapshotMap<TKey, TValue>::size() const
{
    return size(false);
}

template <CHashableKey TKey, typename TValue>
std::generator<const TKey&> DoubleBufferedSnapshotMap<TKey, TValue>::Keys() const
{
    return Keys(false);
}

template <CHashableKey TKey, typename TValue>
std::generator<const TValue&> DoubleBufferedSnapshotMap<TKey, TValue>::Values() const
{
    return Values(false);
}

template <CHashableKey TKey, typename TValue>
void DoubleBufferedSnapshotMap<TKey, TValue>::Clear()
{
    {
        std::lock_guard hotLock(_hotMutex);
        _hotItems.clear();
    }
    std::unique_lock lock(_writeMutex);
    _writeBuffer->map.clear();
    SwapBuffers();
}
