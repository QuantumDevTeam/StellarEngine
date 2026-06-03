// StellarEngine
// Copyright (c) 2026 QuantumDevTeam
// SPDX-License-Identifier: PolyForm-Noncommercial-1.0.0
#pragma once

#include "Event.h"
#include "Label.h"
#include "Quantization/IMetaQuant.h"
#include "Data/Collections/DoubleBuffered/DoubleBufferedSnapshotMap.h"

namespace Stellar::Native::EventSystem
{
    using EventQueueDataType = Core::Data::Collections::DoubleBufferedSnapshotIdentifierMap<Event>;
    
    class EventQueue : Core::Quantization::IMetaQuant
    {
        STELLAR_GENERATE_BODY(EventQueue, IMetaQuant)

    private:
        Core::Label _label = Core::UnnamedUnboundLabel;
        EventQueueDataType _events{};

    public:
        explicit EventQueue(const Core::Label& label);
        explicit EventQueue(std::string_view name);

        PropertyGetter(Core::Identifier, UID) override { return _label.GetUID(); }
        PropertyGetter(Core::Label) { return _label; }
        
        void Enqueue(const Event& event);
        std::shared_ptr<const EventQueueDataType::DataSnapshot> DequeueAll();

        [[nodiscard]] bool Contains(const Core::Identifier& key) const;
        [[nodiscard]] size_t size() const;
        
        void Clear();

        STELLAR_OVERRIDE_DEFAULTS(EventQueue)
    };
}

STELLAR_GENERATE_DEFAULTS(Stellar::Native::EventSystem, EventQueue)
