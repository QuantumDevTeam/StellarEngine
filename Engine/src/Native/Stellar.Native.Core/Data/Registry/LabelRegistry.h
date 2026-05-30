// StellarEngine
// Copyright (c) 2026 QuantumDevTeam
// SPDX-License-Identifier: PolyForm-Noncommercial-1.0.0
#pragma once

#include <atomic>
#include <mutex>
#include <optional>
#include <unordered_map>

namespace Stellar::Native::Core
{
    struct Label;
    struct Identifier;
}

namespace Stellar::Native::Core::Data::Registry
{
    struct string_hash
    {
        using is_transparent = void;

        std::size_t operator()(std::string_view sv) const
        {
            return std::hash<std::string_view>{}(sv);
        }

        std::size_t operator()(const std::string& s) const
        {
            return std::hash<std::string>{}(s);
        }
    };

    class LabelRegistry
    {
        struct Data
        {
            std::unordered_map<Identifier, Label> byId;
            std::unordered_map<std::string, Identifier, string_hash, std::equal_to<>> byName;
        };

        std::atomic<std::shared_ptr<const Data>> _data;
        std::mutex _writeMutex;

        LabelRegistry() : _data(std::make_shared<Data>()) // NOLINT(modernize-use-equals-default)
        {
        }

    public:
        bool Register(const Label& label);
        std::optional<Label> Get(const Identifier& id) const;
        std::optional<Label> Get(std::string_view name) const;
        bool Unregister(const Identifier& id);
        bool Unregister(std::string_view name);

        std::vector<Identifier> GetAllIdentifiers() const;
    };
}
