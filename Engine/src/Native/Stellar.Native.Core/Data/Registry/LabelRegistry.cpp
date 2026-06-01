#include "pch.h"
#include "LabelRegistry.h"

#include "../../Label.h"

namespace Stellar::Native::Core::Data::Registry
{
    bool LabelRegistry::Register(const Label& label)
    {
        if (!label.IsBound()) return false;
        if (_byName.Contains(label.Name)) return false;
        if (!_byId.TryAdd(label.UID, label)) return false;
        if (!_byName.TryAdd(label.Name, label.UID))
        {
            Label dummy;
            _byId.TryRemove(label.UID, dummy);
            return false;
        }
        return true;
    }

    std::optional<Label> LabelRegistry::Get(const Identifier& id) const
    {
        return _byId.TryGet(id);
    }

    std::optional<Label> LabelRegistry::Get(std::string_view name) const
    {
        auto idOpt = _byName.TryGet(name);
        if (!idOpt) return std::nullopt;
        return _byId.TryGet(*idOpt);
    }

    bool LabelRegistry::Unregister(const Identifier& id, Label& label)
    {
        if (!_byId.Contains(id)) return false;
        if (_byId.TryRemove(id, label))
        {
            Identifier dummyId;
            return _byName.TryRemove(label.Name, dummyId);
        }
        return false;
    }

    bool LabelRegistry::Unregister(std::string_view name, Label& label)
    {
        if (!_byName.Contains(name)) return false;
        Identifier dummyId;
        if (_byName.TryRemove(name, dummyId))
        {
            return _byId.TryRemove(dummyId, label);
        }
        return false;
    }

    bool LabelRegistry::Contains(const Identifier& key) const
    {
        return _byId.Contains(key);
    }

    size_t LabelRegistry::size() const
    {
        return _byId.size();
    }

    std::vector<Identifier> LabelRegistry::Identifiers() const
    {
        return _byId.Keys();
    }

    std::vector<Label> LabelRegistry::Values() const
    {
        return _byId.Values();
    }
}
