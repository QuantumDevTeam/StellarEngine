#pragma once
#include "../ICollection.h"

#ifdef IS_FLAT
#define MAP ConcurrentFlatMap
#endif
#ifdef IS_UNORDERED
#define MAP ConcurrentUnorderedMap
#endif

template <CHashableKey TKey, typename TValue>
std::size_t MAP<TKey, TValue>::Segment::size() const
{
    return data.size();
}

template <CHashableKey TKey, typename TValue>
std::size_t MAP<TKey, TValue>::GetSegmentIndex(const TKey& key) const noexcept
{
    return std::hash<TKey>{}(key) % Segments.size();
}

template <CHashableKey TKey, typename TValue>
MAP<TKey, TValue>::MAP(size_t numSegments)
{
    size_t segmentCount = std::max<size_t>(1, numSegments);
    Segments.reserve(segmentCount);
    for (size_t i = 0; i < segmentCount; ++i)
    {
        Segments.push_back(std::make_unique<Segment>());
    }
}

template <CHashableKey TKey, typename TValue>
bool MAP<TKey, TValue>::TryAdd(const TKey& key, const TValue& value)
{
    auto& seg = Segments[GetSegmentIndex(key)];
    std::unique_lock lock(seg->mutex);
    return seg->data.try_emplace(key, value).second;
}

template <CHashableKey TKey, typename TValue>
std::optional<TValue> MAP<TKey, TValue>::TryGet(const TKey& key) const
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

template <CHashableKey TKey, typename TValue>
bool MAP<TKey, TValue>::TryRemove(const TKey& key, TValue& outValue)
{
    auto& seg = Segments[GetSegmentIndex(key)];
    std::unique_lock lock(seg->mutex);
    auto it = seg->data.find(key);
    if (it == seg->data.end()) return false;
    outValue = std::move(it->second);
    seg->data.erase(it);
    return true;
}

template <CHashableKey TKey, typename TValue>
bool MAP<TKey, TValue>::Contains(const TKey& key) const
{
    auto& seg = Segments[GetSegmentIndex(key)];
    std::shared_lock lock(seg->mutex);
    return seg->data.find(key) != seg->data.end();
}

template <CHashableKey TKey, typename TValue>
size_t MAP<TKey, TValue>::size() const
{
    size_t total = 0;
    for (const auto& seg : Segments)
    {
        std::shared_lock lock(seg->mutex);
        total += seg->data.size();
    }
    return total;
}

template <CHashableKey TKey, typename TValue>
std::generator<const TKey&> MAP<TKey, TValue>::Keys() const
{
    for (auto& seg : Segments)
    {
        std::shared_lock lock(seg->mutex);
        for (auto& [k, v] : seg->data)
            co_yield k;
    }
}

template <CHashableKey TKey, typename TValue>
std::generator<const TValue&> MAP<TKey, TValue>::Values() const
{
    for (auto& seg : Segments)
    {
        std::shared_lock lock(seg->mutex);
        for (auto& [k, v] : seg->data)
            co_yield v;
    }
}

template <CHashableKey TKey, typename TValue>
template <typename TReturn, CIterationInvoking<TReturn, TKey, TValue> F>
std::generator<TReturn> MAP<TKey, TValue>::ForEachReadOnly(F action) const
{
    using ReturnType = std::invoke_result_t<F, const TKey&, const TValue&>;
    constexpr bool is_opt = INTERNAL::_is_opt<ReturnType>::value;

    for (auto& seg : Segments)
    {
        std::vector<TReturn> local_buffer;

        {
            std::shared_lock lock(seg->mutex);
            local_buffer.reserve(seg->data.size());

            for (auto& [k, v] : seg->data)
            {
                if constexpr (is_opt)
                {
                    if (auto res = action(k, v); res.has_value())
                    {
                        local_buffer.push_back(std::move(*res));
                    }
                }
                else
                {
                    local_buffer.push_back(action(k, v));
                }
            }
        }

        for (auto& item : local_buffer)
        {
            co_yield std::move(item);
        }
    }
}

template <CHashableKey TKey, typename TValue>
template <CPredicate<TKey, TValue> Pred>
std::generator<std::pair<TKey, TValue>> MAP<TKey, TValue>::ForEachPredicateRemove(Pred pred)
{
    for (auto& seg : Segments)
    {
        std::vector<std::pair<TKey, TValue>> local_buffer;

        {
            std::unique_lock lock(seg->mutex);

            auto it = seg->data.begin();
            while (it != seg->data.end())
            {
                if (pred(it->first, it->second))
                {
                    local_buffer.push_back(
                        std::make_pair(std::move(it->first), std::move(it->second))
                    );
                    it = seg->data.erase(it);
                }
                else
                {
                    ++it;
                }
            }
        }

        for (auto& pair : local_buffer)
        {
            co_yield std::move(pair);
        }
    }
}

template <CHashableKey TKey, typename TValue>
void MAP<TKey, TValue>::Clear()
{
    for (auto& seg : Segments)
    {
        std::unique_lock lock(seg->mutex);
        seg->data.clear();
    }
}
