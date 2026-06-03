// StellarEngine
// Copyright (c) 2026 QuantumDevTeam
// SPDX-License-Identifier: PolyForm-Noncommercial-1.0.0
#pragma once

namespace Stellar::Native::Core::Data::Collections
{
    enum class DataContainerType : char
    {
        // Unordered,
        // Flat,
        
        SegmentedUnordered,
        SegmentedFlat,

        DoubleBuffered,
    };

    inline const char* to_string(DataContainerType e)
    {
        switch (e)
        {
        // case DataContainerType::Unordered: return "Unordered";
        // case DataContainerType::Flat: return "Flat";
        case DataContainerType::SegmentedUnordered: return "SegmentedUnordered";
        case DataContainerType::SegmentedFlat: return "SegmentedFlat";
        case DataContainerType::DoubleBuffered: return "DoubleBuffered";
        default: std::unreachable();
        }
    }
}
