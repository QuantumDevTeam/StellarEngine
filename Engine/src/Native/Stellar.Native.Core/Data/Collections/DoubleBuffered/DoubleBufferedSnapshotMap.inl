#pragma once

template <HashableKey TKey, typename TValue>
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

template <HashableKey TKey, typename TValue>
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

template <HashableKey TKey, typename TValue>
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

template <HashableKey TKey, typename TValue>
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

template <HashableKey TKey, typename TValue>
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

template <HashableKey TKey, typename TValue>
std::vector<TKey> DoubleBufferedSnapshotMap<TKey, TValue>::Keys(bool immediate) const
{
    auto snapshot = GetSnapshot();
    std::vector<TKey> result;
    if (immediate)
    {
        std::lock_guard hotLock(_hotMutex);
        result.reserve(snapshot->map.size() + _hotItems.size());
        for (const auto& [key, _] : _hotItems)
            result.push_back(key);
    }
    else
    {
        result.reserve(snapshot->map.size());
    }
    for (const auto& [key, _] : snapshot->map)
        result.push_back(key);
    return result;
}

template <HashableKey TKey, typename TValue>
std::vector<TValue> DoubleBufferedSnapshotMap<TKey, TValue>::Values(bool immediate) const
{
    auto snapshot = GetSnapshot();
    std::vector<TValue> result;
    if (immediate)
    {
        std::lock_guard hotLock(_hotMutex);
        result.reserve(snapshot->map.size() + _hotItems.size());
        for (const auto& [_, value] : snapshot->map)
            result.push_back(value);
        for (const auto& [_, value] : _hotItems)
            result.push_back(value.value());
    }
    else
    {
        result.reserve(snapshot->map.size());
        for (const auto& [_, value] : snapshot->map)
            result.push_back(value);
    }
    return result;
}

template <HashableKey TKey, typename TValue>
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

template <HashableKey TKey, typename TValue>
bool DoubleBufferedSnapshotMap<TKey, TValue>::TryAdd(const TKey& key, const TValue& value)
{
    return TryAdd(key, value, false);
}

template <HashableKey TKey, typename TValue>
std::optional<TValue> DoubleBufferedSnapshotMap<TKey, TValue>::TryGet(const TKey& key) const
{
    return TryGet(key, false);
}

template <HashableKey TKey, typename TValue>
bool DoubleBufferedSnapshotMap<TKey, TValue>::TryRemove(const TKey& key, TValue& outValue)
{
    return TryRemove(key, outValue, false);
}

template <HashableKey TKey, typename TValue>
bool DoubleBufferedSnapshotMap<TKey, TValue>::Contains(const TKey& key) const
{
    return Contains(key, false);
}

template <HashableKey TKey, typename TValue>
size_t DoubleBufferedSnapshotMap<TKey, TValue>::size() const
{
    return size(false);
}

template <HashableKey TKey, typename TValue>
std::vector<TKey> DoubleBufferedSnapshotMap<TKey, TValue>::Keys() const
{
    return Keys(false);
}

template <HashableKey TKey, typename TValue>
std::vector<TValue> DoubleBufferedSnapshotMap<TKey, TValue>::Values() const
{
    return Values(false);
}

template <HashableKey TKey, typename TValue>
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
