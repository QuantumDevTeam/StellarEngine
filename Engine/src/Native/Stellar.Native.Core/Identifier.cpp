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
        if (FAILED(::CoCreateGuid(&guid))) // TODO:: HRESULT checking
        {
            return Identifier{};
        }
        return FromNativeGUID(guid);
    }

    Identifier Identifier::FromString(std::string_view str)
    {
        GUID guid;
        if (FAILED(::CLSIDFromString(std::wstring(str.begin(), str.end()).c_str(), &guid)))
        {
            return Null();
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

    bool Identifier::IsNull() const
    {
        return data.data() == nullptr;
    }

    std::string Identifier::ToString() const
    {
        GUID native = ToNativeGUID();
        LPOLESTR str = nullptr;
        if (SUCCEEDED(::StringFromCLSID(native, &str))) // TODO:: HRESULT checking
        {
            std::wstring ws(str);
            CoTaskMemFree(str);
            return {ws.begin(), ws.end()};
        }
        return {};
    }

    std::size_t Identifier::Hash::operator()(const Identifier& id) const noexcept
    {
        const uint64_t* p = reinterpret_cast<const uint64_t*>(id.data.data());
        return p[0] ^ p[1] + 0x9e3779b9 + (p[0] << 6) + (p[0] >> 2);
    }
}
