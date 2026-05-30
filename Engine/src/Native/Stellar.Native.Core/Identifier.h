// StellarEngine
// Copyright (c) 2026 QuantumDevTeam
// SPDX-License-Identifier: PolyForm-Noncommercial-1.0.0
#pragma once

namespace Stellar::Native::Core
{
    struct Identifier
    {
    private:
        static Identifier FromNativeGUID(const GUID& guid);
        GUID ToNativeGUID() const;

    public:
        std::array<uint8_t, 16> data;

        // null
        constexpr Identifier() : data{}
        {
        }

        // from data
        explicit constexpr Identifier(const std::array<uint8_t, 16>& bytes) : data(bytes)
        {
        }

        // little-endian
        explicit constexpr Identifier(uint64_t high, uint64_t low)
        {
            std::copy_n(reinterpret_cast<const uint8_t*>(&high), 8, data.begin());
            std::copy_n(reinterpret_cast<const uint8_t*>(&low), 8, data.begin() + 8);
        }

        // null identifier
        static constexpr Identifier Null()
        {
            return Identifier{};
        }

        static Identifier Create();
        static Identifier FromString(std::string_view str);

        static constexpr Identifier FromBytes(const std::array<uint8_t, 16>& bytes);
        static constexpr Identifier FromBytes(std::span<const uint8_t, 16> bytes);

        bool IsNull() const;
        std::string ToString() const;

        auto operator<=>(const Identifier&) const = default;

        struct Hash
        {
            std::size_t operator()(const Identifier& id) const noexcept;
        };
    };

    inline constexpr Identifier NullIdentifier = Identifier::Null();

    std::string to_string(Identifier id_);
}
