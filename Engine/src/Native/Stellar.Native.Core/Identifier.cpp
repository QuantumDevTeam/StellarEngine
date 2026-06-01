#include "pch.h"
#include "Identifier.h"

#include <format>

namespace Stellar::Native::Core
{
    Identifier Identifier::FromNativeGUID(const GUID& guid)
    {
        Identifier id;
        std::copy_n(reinterpret_cast<const uint8_t*>(&guid), sizeof(GUID), id.data.begin());
        return id;
    }

    GUID Identifier::ToNativeGUID() const
    {
        GUID guid;
        std::copy_n(data.data(), sizeof(GUID), reinterpret_cast<uint8_t*>(&guid));
        return guid;
    }

    Identifier Identifier::Create()
    {
        GUID guid;
        // TODO:: HRESULT checking
        if (FAILED(::CoCreateGuid(&guid)))
        {
            return NullIdentifier;
        }
        return FromNativeGUID(guid);
    }

    Identifier Identifier::FromString(std::string_view str)
    {
        GUID guid;
        // TODO:: HRESULT checking
        if (FAILED(::CLSIDFromString(std::wstring(str.begin(), str.end()).c_str(), &guid)))
        {
            return NullIdentifier;
        }
        return FromNativeGUID(guid);
    }

    constexpr Identifier Identifier::FromBytes(const std::array<uint8_t, 16>& bytes)
    {
        return Identifier(bytes);
    }

    constexpr Identifier Identifier::FromBytes(std::span<const uint8_t, 16> bytes)
    {
        std::array<uint8_t, 16> arr;
        std::copy_n(bytes.data(), 16, arr.begin());
        return Identifier(arr);
    }

    bool Identifier::IsNull() const noexcept
    {
        return data == std::array<uint8_t, 16>();
    }

    std::string Identifier::ToString() const noexcept
    {
        auto& d = data;
        return std::format(
            "{:02X}{:02X}{:02X}{:02X}-{:02X}{:02X}-{:02X}{:02X}-{:02X}{:02X}-{:02X}{:02X}{:02X}{:02X}{:02X}{:02X}",
            d[0], d[1], d[2], d[3], d[4], d[5], d[6], d[7], d[8], d[9], d[10], d[11], d[12], d[13], d[14], d[15]);
    }

    uint64_t Identifier::GetHashCode() const noexcept
    {
        const uint64_t* p = reinterpret_cast<const uint64_t*>(data.data());
        return p[0] ^ p[1] + 0x9e3779b9 + (p[0] << 6) + (p[0] >> 2);
    }
}
