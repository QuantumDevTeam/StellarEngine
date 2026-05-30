#include "pch.h"
#include "LabelRegistry.h"

#include "../../Identifier.h"
#include "../../Label.h"

namespace Stellar::Native::Core::Data::Registry
{
    bool LabelRegistry::Register(const Label& label)
    {
        if (!label.IsBound()) return false;
        std::lock_guard lock(_writeMutex);
        auto oldData = _data.load(std::memory_order_acquire);
        if (oldData->byName.contains(label.Name))
            return false;
        auto newData = std::make_shared<Data>(*oldData);
        newData->byId.emplace(label.UID, label);
        newData->byName.emplace(label.Name, label.UID);
        _data.store(newData, std::memory_order_release);
        return true;
    }

    std::optional<Label> LabelRegistry::Get(const Identifier& id) const
    {
        auto data = _data.load(std::memory_order_acquire);
        auto it = data->byId.find(id);
        if (it != data->byId.end())
            return it->second;
        return std::nullopt;
    }

    std::optional<Label> LabelRegistry::Get(std::string_view name) const
    {
        auto data = _data.load(std::memory_order_acquire);
        auto itName = data->byName.find(name);
        if (itName == data->byName.end())
            return std::nullopt;
        auto itId = data->byId.find(itName->second);
        if (itId != data->byId.end())
            return itId->second;
        return std::nullopt;
    }

    bool LabelRegistry::Unregister(const Identifier& id)
    {
        std::lock_guard lock(_writeMutex);
        auto oldData = _data.load(std::memory_order_acquire);
        auto itId = oldData->byId.find(id);
        if (itId == oldData->byId.end())
            return false;
        auto newData = std::make_shared<Data>(*oldData);
        std::string name = itId->second.Name;
        newData->byId.erase(id);
        newData->byName.erase(name);
        _data.store(newData, std::memory_order_release);
        return true;
    }

    bool LabelRegistry::Unregister(std::string_view name)
    {
        std::lock_guard lock(_writeMutex);
        auto oldData = _data.load(std::memory_order_acquire);
        auto itName = oldData->byName.find(name);
        if (itName == oldData->byName.end())
            return false;
        auto newData = std::make_shared<Data>(*oldData);
        Identifier id = itName->second;
        newData->byName.erase(name);
        newData->byId.erase(id);
        _data.store(newData, std::memory_order_release);
        return true;
    }

    std::vector<Identifier> LabelRegistry::GetAllIdentifiers() const
    {
        auto data = _data.load(std::memory_order_acquire);
        std::vector<Identifier> result;
        result.reserve(data->byId.size());
        for (auto& [id, _] : data->byId)
            result.push_back(id);
        return result;
    }
}
