#pragma once

template <HashableKey TKey, typename TValue>
std::size_t ConcurrentUnorderedMap<TKey, TValue>::Segment::size() const
{
    return data.size();
}

template <HashableKey TKey, typename TValue>
std::size_t ConcurrentUnorderedMap<TKey, TValue>::GetSegmentIndex(const TKey& key) const noexcept
{
    return std::hash<TKey>{}(key) % Segments.size();
}

template <HashableKey TKey, typename TValue>
ConcurrentUnorderedMap<TKey, TValue>::ConcurrentUnorderedMap(size_t numSegments)
{
    size_t segmentCount = std::max<size_t>(1, numSegments);
    Segments.reserve(segmentCount);
    for (size_t i = 0; i < segmentCount; ++i)
    {
        Segments.push_back(std::make_unique<Segment>());
    }
}

template <HashableKey TKey, typename TValue>
bool ConcurrentUnorderedMap<TKey, TValue>::TryAdd(const TKey& key, const TValue& value)
{
    auto& seg = Segments[GetSegmentIndex(key)];
    std::unique_lock lock(seg->mutex);
    return seg->data.try_emplace(key, value).second;
}

template <HashableKey TKey, typename TValue>
std::optional<TValue> ConcurrentUnorderedMap<TKey, TValue>::TryGet(const TKey& key) const
{
    auto& seg = Segments[GetSegmentIndex(key)];
    std::shared_lock lock(seg->mutex);
    auto it = seg->data.find(key);
    if (it != seg->data.end())
    {
        return it->second;
    }
    return std::nullopt;
}

template <HashableKey TKey, typename TValue>
bool ConcurrentUnorderedMap<TKey, TValue>::TryRemove(const TKey& key, TValue& outValue)
{
    auto& seg = Segments[GetSegmentIndex(key)];
    std::unique_lock lock(seg->mutex);
    auto it = seg->data.find(key);
    if (it == seg->data.end()) return false;
    outValue = std::move(it->second);
    seg->data.erase(it);
    return true;
}

template <HashableKey TKey, typename TValue>
bool ConcurrentUnorderedMap<TKey, TValue>::Contains(const TKey& key) const
{
    auto& seg = Segments[GetSegmentIndex(key)];
    std::shared_lock lock(seg->mutex);
    return seg->data.find(key) != seg->data.end();
}

template <HashableKey TKey, typename TValue>
size_t ConcurrentUnorderedMap<TKey, TValue>::size() const
{
    size_t total = 0;
    for (const auto& seg : Segments)
    {
        std::shared_lock lock(seg->mutex);
        total += seg->data.size();
    }
    return total;
}

template <HashableKey TKey, typename TValue>
std::generator<const TKey&> ConcurrentUnorderedMap<TKey, TValue>::Keys() const
{
    for (auto& seg : Segments)
    {
        std::shared_lock lock(seg->mutex);
        for (auto& [k, v] : seg->data)
            co_yield k;
    }
}

template <HashableKey TKey, typename TValue>
std::generator<const TValue&> ConcurrentUnorderedMap<TKey, TValue>::Values() const
{
    for (auto& seg : Segments)
    {
        std::shared_lock lock(seg->mutex);
        for (auto& [k, v] : seg->data)
            co_yield v;
    }
}

template <HashableKey TKey, typename TValue>
void ConcurrentUnorderedMap<TKey, TValue>::Clear()
{
    for (auto& seg : Segments)
    {
        std::unique_lock lock(seg->mutex);
        seg->data.clear();
    }
}
