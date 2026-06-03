// StellarEngine
// Copyright (c) 2026 QuantumDevTeam
// SPDX-License-Identifier: PolyForm-Noncommercial-1.0.0
#pragma once
STELLAR_CLANG_IGNORE("-Wpadded")

#include <functional>

#include "Event.h"
#include "EventQueue.h"
#include "Data/Collections/ConcurrentMap/ConcurrentUnorderedMap.h"

namespace Stellar::Native::EventSystem
{
    // TODO: beautify and add concept

    struct Handler
    {
        STELLAR_GENERATE_BODY_PARTIAL(Handler)

        float Priority;
        std::function<void(const Event&)> Callback;

        Handler(float priority, std::function<void(const Event&)> callback)
            : Priority(priority), Callback(std::move(callback))
        {
        }

        bool operator<(const Handler& other) const { return Priority > other.Priority; }
    };

    class EventBus : Core::Quantization::IMetaQuant
    {
        STELLAR_GENERATE_BODY(EventBus, IMetaQuant)

    private:
        Core::Identifier _uid;

        // TODO: To FlatMaps
        Core::Data::Collections::ConcurrentUnorderedIdentifierMap<std::vector<Handler>> _subscriptions;
        Core::Data::Collections::ConcurrentUnorderedIdentifierMap<EventQueue*> _queues;

    public:
        explicit EventBus(Core::Identifier uid);

        PropertyGetter(Core::Identifier, UID) { return _uid; }

        void AddQueue(EventQueue* queue);
        EventQueue* GetQueue(const Core::Identifier& id) const;
        EventQueue* RemoveQueue(const Core::Identifier& id);

        void Subscribe(const EventType& type, std::function<void(const Event&)> callback, float priority = 0.0f);
        void Unsubscribe(const EventType& type);

        void Emit(const Event& event) const;

        void Enqueue(const Core::Identifier& queueId, const Event& event) const;
        void ProcessQueue(const Core::Identifier& queueId) const;

        void Clear(bool withQueue = true);

        STELLAR_OVERRIDE_DEFAULTS(EventBus)
    };
}

STELLAR_GENERATE_DEFAULTS(Stellar::Native::EventSystem, EventBus)

STELLAR_CLANG_IGNORE_END()
