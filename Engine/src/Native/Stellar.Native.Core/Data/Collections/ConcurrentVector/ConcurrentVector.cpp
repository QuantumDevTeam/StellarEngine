#include "pch.h"
#include "ConcurrentVector.h"

#include <algorithm>

namespace Stellar::Native::Core::Data::Collections
{
    template <typename T>
    void ConcurrentVector<T>::PushBack(const T& value)
    {
        std::unique_lock lock(_mutex);
        _data.push_back(value);
    }

    template <typename T>
    bool ConcurrentVector<T>::TryGet(size_t index, T& outValue) const
    {
        std::shared_lock lock(_mutex);
        if (index >= _data.size()) return false;
        outValue = _data[index];
        return true;
    }

    template <typename T>
    bool ConcurrentVector<T>::TryRemove(size_t index, T& outValue)
    {
        std::unique_lock lock(_mutex);
        if (index >= _data.size()) return false;
        outValue = std::move(_data[index]);
        _data.erase(_data.begin() + index);
        return true;
    }

    template <typename T>
    void ConcurrentVector<T>::Sort(std::function<bool(const T&, const T&)> comparator)
    {
        std::unique_lock lock(_mutex);
        std::sort(_data.begin(), _data.end(), comparator);
    }

    template <typename T>
    size_t ConcurrentVector<T>::size() const
    {
        std::shared_lock lock(_mutex);
        return _data.size();
    }

    template <typename T>
    void ConcurrentVector<T>::Clear()
    {
        std::unique_lock lock(_mutex);
        _data.clear();
    }
}
