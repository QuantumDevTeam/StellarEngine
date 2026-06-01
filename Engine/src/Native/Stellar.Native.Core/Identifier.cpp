#include "pch.h"
#include "Identifier.h"

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
        GUID native = ToNativeGUID();
        LPOLESTR str = nullptr;
        // TODO:: HRESULT checking
        if (SUCCEEDED(::StringFromCLSID(native, &str)))
        {
            std::wstring ws(str);
            CoTaskMemFree(str);
            return "Identifier#" + std::string(ws.begin(), ws.end());
        }
        return {};
    }

    uint64_t Identifier::GetHashCode() const noexcept
    {
        const uint64_t* p = reinterpret_cast<const uint64_t*>(data.data());
        return p[0] ^ p[1] + 0x9e3779b9 + (p[0] << 6) + (p[0] >> 2);
    }
}
