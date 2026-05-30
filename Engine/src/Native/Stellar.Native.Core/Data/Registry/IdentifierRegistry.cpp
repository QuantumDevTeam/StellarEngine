#include "pch.h"
#include "IdentifierRegistry.h"

#include "../../Identifier.h"

namespace Stellar::Native::Core::Data::Registry
{
    bool IdentifierRegistry::Register(const Identifier& id)
    {
        std::lock_guard lock(_writeMutex);
        auto oldData = _data.load(std::memory_order_acquire);
        auto newData = std::make_shared<Data>(*oldData);
        auto [it, inserted] = newData->map.try_emplace(id, id);
        if (!inserted) return false;
        _data.store(newData, std::memory_order_release);
        return true;
    }

    std::optional<Identifier> IdentifierRegistry::Get(const Identifier& id) const
    {
        auto data = _data.load(std::memory_order_acquire);
        auto it = data->map.find(id);
        if (it != data->map.end())
            return it->second;
        return std::nullopt;
    }

    bool IdentifierRegistry::Unregister(const Identifier& id)
    {
        std::lock_guard lock(_writeMutex);
        auto oldData = _data.load(std::memory_order_acquire);
        auto newData = std::make_shared<Data>(*oldData);
        if (newData->map.erase(id) == 0) return false;
        _data.store(newData, std::memory_order_release);
        return true;
    }
}
