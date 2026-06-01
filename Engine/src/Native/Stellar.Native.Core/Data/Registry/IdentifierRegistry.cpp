#include "pch.h"
#include "IdentifierRegistry.h"

namespace Stellar::Native::Core::Data::Registry
{
    bool IdentifierRegistry::Register(const Identifier& id)
    {
        return _identifiers.TryAdd(id, id);
    }

    std::optional<Identifier> IdentifierRegistry::Get(const Identifier& id) const
    {
        return _identifiers.TryGet(id);
    }

    bool IdentifierRegistry::Unregister(const Identifier& id, Identifier& outValue)
    {
        return _identifiers.TryRemove(id, outValue);
    }

    bool IdentifierRegistry::Contains(const Identifier& key) const
    {
        return _identifiers.Contains(key);
    }

    size_t IdentifierRegistry::size() const
    {
        return _identifiers.size();
    }

    std::generator<const Identifier&> IdentifierRegistry::Identifiers() const
    {
        return _identifiers.Keys();
    }
}
