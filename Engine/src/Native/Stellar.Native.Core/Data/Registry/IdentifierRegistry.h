// StellarEngine
// Copyright (c) 2026 QuantumDevTeam
// SPDX-License-Identifier: PolyForm-Noncommercial-1.0.0
#pragma once

#include <memory>
#include <atomic>
#include <mutex>
#include <optional>
#include <unordered_map>

namespace Stellar::Native::Core
{
    struct Identifier;
}

namespace Stellar::Native::Core::Data::Registry
{
    class IdentifierRegistry
    {
        struct Data
        {
            std::unordered_map<Identifier, Identifier> map;
        };

        std::atomic<std::shared_ptr<const Data>> _data;
        std::mutex _writeMutex;

        IdentifierRegistry() : _data(std::make_shared<Data>()) // NOLINT(modernize-use-equals-default)
        {
        }

    public:
        static IdentifierRegistry& Instance()
        {
            static IdentifierRegistry instance;
            return instance;
        }

        bool Register(const Identifier& id);
        std::optional<Identifier> Get(const Identifier& id) const;
        bool Unregister(const Identifier& id);
    };
}
