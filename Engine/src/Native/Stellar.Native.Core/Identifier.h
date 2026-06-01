// StellarEngine
// Copyright (c) 2026 QuantumDevTeam
// SPDX-License-Identifier: PolyForm-Noncommercial-1.0.0
#pragma once

#include <charconv>


namespace Stellar::Native::Core
{
    struct Identifier final
    {
    private:
        [[nodiscard]] static Identifier FromNativeGUID(const GUID& guid); // TODO: multiple instances
        [[nodiscard]] GUID ToNativeGUID() const;

    public:
        std::array<uint8_t, 16> data{};

        // null
        constexpr Identifier() = default;

        // from data
        explicit constexpr Identifier(const std::array<uint8_t, 16>& bytes)
            : data(bytes)
        {
        }

        // from string
        explicit constexpr Identifier(std::string_view str)
        {
            uint32_t p1 = 0, p2 = 0, p3 = 0, p4 = 0;
            uint64_t p5 = 0;

            auto r1 = std::from_chars(str.data() + 0, str.data() + 8, p1, 16);
            auto r2 = std::from_chars(str.data() + 9, str.data() + 13, p2, 16);
            auto r3 = std::from_chars(str.data() + 14, str.data() + 18, p3, 16);
            auto r4 = std::from_chars(str.data() + 19, str.data() + 23, p4, 16);
            auto r5 = std::from_chars(str.data() + 24, str.data() + 36, p5, 16);

            if (r1.ec != std::errc{} || r2.ec != std::errc{} ||
                r3.ec != std::errc{} || r4.ec != std::errc{} || r5.ec != std::errc{})
            {
                throw std::exception("Invalid GUID format string");
            }

            data[0] = p1 >> 24 & 0xFF;
            data[1] = p1 >> 16 & 0xFF;
            data[2] = p1 >> 8 & 0xFF;
            data[3] = p1 & 0xFF;
            data[4] = p2 >> 8 & 0xFF;
            data[5] = p2 & 0xFF;
            data[6] = p3 >> 8 & 0xFF;
            data[7] = p3 & 0xFF;
            data[8] = p4 >> 8 & 0xFF;
            data[9] = p4 & 0xFF;
            data[10] = p5 >> 40 & 0xFF;
            data[11] = p5 >> 32 & 0xFF;
            data[12] = p5 >> 24 & 0xFF;
            data[13] = p5 >> 16 & 0xFF;
            data[14] = p5 >> 8 & 0xFF;
            data[15] = p5 & 0xFF;
        }

        // little-endian
        explicit constexpr Identifier(uint64_t high, uint64_t low)
        {
            std::copy_n(reinterpret_cast<const uint8_t*>(&high), 8, data.begin());
            std::copy_n(reinterpret_cast<const uint8_t*>(&low), 8, data.begin() + 8);
        }

        // null identifier
        [[nodiscard]] static constexpr Identifier Null() { return {}; }

        // gets new random identifier
        [[nodiscard]] static Identifier Create();

        // create new identifier from gotten string
        [[nodiscard]] static Identifier FromString(std::string_view str);
        // create new identifier from gotten bytes, presents in array
        [[nodiscard]] static constexpr Identifier FromBytes(const std::array<uint8_t, 16>& bytes);
        // create new identifier from gotten bytes, presents in span
        [[nodiscard]] static constexpr Identifier FromBytes(std::span<const uint8_t, 16> bytes);

        // check identifier on null UID
        [[nodiscard]] bool IsNull() const noexcept;

        STELLAR_DEFINE_CS_METHODS();

        auto operator<=>(const Identifier&) const noexcept = default;
    };

    inline constexpr Identifier NullIdentifier = {};

    STELLAR_DEFINE_TO_STRING(Identifier);
}

STELLAR_DEFINE_HASHER(Stellar::Native::Core::Identifier);
