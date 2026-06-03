#include "pch.h"
#include "LabelRegistry.h"

#include "../../Label.h"

namespace Stellar::Native::Core::Data::Registry
{
    bool LabelRegistry::Register(const Label& label)
    {
        if (!label.IsBound()) return false;
        if (_byName.Contains(label.GetName())) return false;
        if (!_byId.TryAdd(label.GetUID(), label)) return false;
        if (!_byName.TryAdd(label.GetName(), label.GetUID()))
        {
            Label dummy;
            _byId.TryRemove(label.GetUID(), dummy);
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
            return _byName.TryRemove(label.GetName(), dummyId);
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

    std::generator<const Identifier&> LabelRegistry::Identifiers() const
    {
        return _byId.Keys();
    }

    std::generator<const Label&> LabelRegistry::Values() const
    {
        return _byId.Values();
    }
}
