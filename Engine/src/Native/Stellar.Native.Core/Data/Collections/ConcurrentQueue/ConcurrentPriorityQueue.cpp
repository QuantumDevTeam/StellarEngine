#include "pch.h"
#include "ConcurrentPriorityQueue.h"

namespace Stellar::Native::Core::Data::Collections
{
    template <typename T, typename Compare>
    std::size_t ConcurrentPriorityQueue<T, Compare>::GetSegmentIndex(const T& value) const noexcept
    {
        return std::hash<T>()(value) % _segments.size();
    }

    template <typename T, typename Compare>
    ConcurrentPriorityQueue<T, Compare>::ConcurrentPriorityQueue(size_t numSegments)
        : _segments(numSegments)
    {
    }

    template <typename T, typename Compare>
    void ConcurrentPriorityQueue<T, Compare>::Push(T value)
    {
        size_t idx = _hasher(value) % _segments.size();
        auto& seg = _segments[idx];
        {
            std::lock_guard lock(seg.mutex);
            seg.heap.push(std::move(value));
        }
        seg.cv.notify_one();
    }

    template <typename T, typename Compare>
    std::optional<T> ConcurrentPriorityQueue<T, Compare>::TryPop()
    {
        size_t bestIdx = _segments.size();
        T bestValue;
        bool found = false;

        for (size_t i = 0; i < _segments.size(); ++i)
        {
            auto& seg = _segments[i];
            std::unique_lock lock(seg.mutex, std::try_to_lock);
            if (!lock.owns_lock()) continue;
            if (seg.heap.empty()) continue;
            const T& top = seg.heap.top();
            if (!found || Compare()(bestValue, top))
            {
                bestValue = top;
                bestIdx = i;
                found = true;
            }
        }
        if (!found) return std::nullopt;

        auto& bestSeg = _segments[bestIdx];
        std::lock_guard lock(bestSeg.mutex);
        T result = std::move(const_cast<T&>(bestSeg.heap.top()));
        bestSeg.heap.pop();
        return result;
    }

    template <typename T, typename Compare>
    T ConcurrentPriorityQueue<T, Compare>::PopWait(const std::stop_token& st)
    {
        while (true)
        {
            if (auto val = TryPop()) return std::move(*val);
            for (auto& seg : _segments)
            {
                std::unique_lock lock(seg.mutex);
                if (!seg.heap.empty()) break;
                if (seg.cv.wait(lock, st, [&] { return !seg.heap.empty(); }))
                {
                    T val = std::move(const_cast<T&>(seg.heap.top()));
                    seg.heap.pop();
                    return val;
                }
                if (st.stop_requested()) return T{};
            }
        }
    }

    template <typename T, typename Compare>
    size_t ConcurrentPriorityQueue<T, Compare>::size() const
    {
        size_t total = 0;
        for (auto& seg : _segments)
        {
            std::shared_lock lock(seg.mutex);
            total += seg.heap.size();
        }
        return total;
    }

    template <typename T, typename Compare>
    void ConcurrentPriorityQueue<T, Compare>::Clear()
    {
        for (auto& seg : _segments)
        {
            std::lock_guard lock(seg.mutex);
            decltype(seg.heap) empty;
            seg.heap.swap(empty);
        }
    }

    template <typename T, typename Compare>
    uint64_t ConcurrentPriorityQueue<T, Compare>::GetHashCode() const noexcept
    {
        return _uid.GetHashCode();
    }

    template <typename T, typename Compare>
    std::string ConcurrentPriorityQueue<T, Compare>::ToString() const noexcept
    {
        return std::format(
            "{}"
            "#UID({})",
            StaticClassName(),
            _uid.ToString()
        );
    }
}
