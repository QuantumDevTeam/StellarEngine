// StellarEngine
// Copyright (c) 2026 QuantumDevTeam
// SPDX-License-Identifier: PolyForm-Noncommercial-1.0.0
#pragma once

#include <optional>

#include "../Collections/ConcurrentMap/ConcurrentUnorderedMap.h"
#include "../Collections/DoubleBuffered/DoubleBufferedSnapshotMap.h"

namespace Stellar::Native::Core
{
    struct Label;
}

namespace Stellar::Native::Core::Data::Registry
{
    class LabelRegistry final
    {
    public:
        // construction
        constexpr LabelRegistry() noexcept = default;
        ~LabelRegistry() noexcept = default;

        // copy
        LabelRegistry(const LabelRegistry&) = default;
        LabelRegistry(LabelRegistry&&) noexcept = default;
        LabelRegistry& operator=(const LabelRegistry&) = default;
        LabelRegistry& operator=(LabelRegistry&&) noexcept = default;

        // spaceship
        auto operator<=>(const LabelRegistry&) const noexcept = default;

        // singleton
        static LabelRegistry& GetInstance()
        {
            static LabelRegistry instance;
            return instance;
        }
        
    private:
        Collections::ConcurrentUnorderedIdentifierMap<Label> _byId{64};
        Collections::ConcurrentUnorderedMap<std::string_view, Identifier> _byName{};

    public:
        bool Register(const Label& label);
        std::optional<Label> Get(const Identifier& id) const;
        std::optional<Label> Get(std::string_view name) const;
        bool Unregister(const Identifier& id, Label& label);
        bool Unregister(std::string_view name, Label& label);

        [[nodiscard]] bool Contains(const Identifier& key) const;
        [[nodiscard]] size_t size() const;

        [[nodiscard]] std::generator<const Identifier&> Identifiers() const;
        [[nodiscard]] std::generator<const Label&> Values() const;
    };
}
