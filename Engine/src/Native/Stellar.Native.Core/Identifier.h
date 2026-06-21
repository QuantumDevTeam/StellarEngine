// StellarEngine
// Copyright (c) 2026 QuantumDevTeam
// SPDX-License-Identifier: PolyForm-Noncommercial-1.0.0
#pragma once

namespace Stellar::Native::Core
{
    struct Identifier final
    {
        using IdentifierDataFormat = std::array<uint8_t, 16>;

        constexpr Identifier() noexcept = default;
        ~Identifier() noexcept = default;
        STELLAR_DEFAULT_COPY_OPERATORS(Identifier);

    private:
        IdentifierDataFormat _data{};

        [[nodiscard]] static Identifier FromNativeGUID(const GUID& guid);
        [[nodiscard]] GUID ToNativeGUID() const;

    public:
        // from data
        explicit constexpr Identifier(const IdentifierDataFormat& bytes);

        // from string
        explicit constexpr Identifier(std::string_view str);

        // little-endian
        explicit constexpr Identifier(uint64_t high, uint64_t low);

        STELLAR_CLASS_NAME_DEF(Identifier)

        PropertyGetter(const IdentifierDataFormat&, Data) { return _data; }

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

        [[nodiscard]] STELLAR_TO_STRING();
        [[nodiscard]] STELLAR_HASHCODE();

        STELLAR_SPACESHIP(Identifier);
    };

#include "Identifier.inl"

    inline constexpr Identifier NullIdentifier;

    STELLAR_GENERATE_TO_STRING(Identifier)
}

STELLAR_GENERATE_HASHER(Stellar::Native::Core::Identifier)
