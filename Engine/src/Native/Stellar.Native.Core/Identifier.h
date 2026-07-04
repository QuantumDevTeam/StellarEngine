// StellarEngine
// Copyright (c) 2026 QuantumDevTeam
// SPDX-License-Identifier: PolyForm-Noncommercial-1.0.0
#pragma once

#include "_Mixins/Hashable.h"
#include "_Mixins/Stringable.h"

namespace Stellar::Native::Core
{
    struct Identifier final : Stringable<Identifier>, Hashable<Identifier>
    {
        // data type
        using IdentifierDataFormat = std::array<uint8_t, 16>;

        // construction
        constexpr Identifier() noexcept = default;
        ~Identifier() noexcept = default;

        // copy
        Identifier(const Identifier&) = default;
        Identifier(Identifier&&) noexcept = default;
        Identifier& operator=(const Identifier&) = default;
        Identifier& operator=(Identifier&&) noexcept = default;

        // spaceship
        auto operator<=>(const Identifier&) const noexcept = default;

    private:
        // data - binary
        IdentifierDataFormat _data{};

        // helpfully method for creating Identifier from GUID
        [[nodiscard]] static Identifier FromNativeGUID(const GUID& guid);
        // helpfully method for getting GUID from identifier
        [[nodiscard]] GUID ToNativeGUID() const;

    public:
        // from data
        explicit constexpr Identifier(const IdentifierDataFormat& bytes);

        // from string
        explicit constexpr Identifier(std::string_view str);

        // little-endian
        explicit constexpr Identifier(uint64_t high, uint64_t low);

        // just data getter
        [[nodiscard]] const IdentifierDataFormat& GetData() const { return _data; }

        // null identifier
        [[nodiscard]] static constexpr Identifier Null() { return {}; }

        // gets new random identifier
        [[nodiscard]] static Identifier Create();
        // create new identifier from gotten string
        [[nodiscard]] static Identifier FromString(std::string_view str);
        // create new identifier from gotten bytes, presents in array
        [[nodiscard]] static constexpr Identifier FromBytes(const IdentifierDataFormat& bytes);
        // create new identifier from gotten bytes, presents in span
        [[nodiscard]] static constexpr Identifier FromBytes(std::span<const uint8_t, 16> bytes);

        // check identifier on null UID
        [[nodiscard]] bool IsNull() const noexcept;

        // managed operations
        [[nodiscard]] std::string ToString() const noexcept override;
        [[nodiscard]] uint64_t GetHashCode() const noexcept override;
    };

#include "Identifier.inl"

    inline constexpr Identifier NullIdentifier = Identifier::Null();
}
