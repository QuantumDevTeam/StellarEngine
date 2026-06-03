// StellarEngine
// Copyright (c) 2026 QuantumDevTeam
// SPDX-License-Identifier: PolyForm-Noncommercial-1.0.0
module;

#include "pch.h"

#include "ICollection.h"
#include "ConcurrentUnordered/ConcurrentUnorderedMap.h"
#include "ConcurrentUnordered/ConcurrentFlatMap.h"
#include "DoubleBuffered/DoubleBufferedSnapshotMap.h"
#include "DataContainer.h"

export module Stellar.Native.Core.Data.Collections;

export namespace Stellar::Native::Core::Data::Collections {
    // not implemented
    // using Collections::to_string;
    
    // ICollection
    
    template <typename Key>
    using Collections::CHashableKey;
    
    template <typename Key>
    using Collections::CSortableKey;
    
    template <typename Key>
    using Collections::CCollection;
    
    template <CHashableKey TKey, typename TValue>
    using Collections::ICollection;
    
    // ConcurrentMap
    
    using Collections::DefaultNumSegments;
    
    template <CHashableKey TKey, typename TValue>
    using Collections::ConcurrentUnorderedMap;
        
    template <typename TValue>
    using Collections::ConcurrentUnorderedIdentifierMap;
    
    template <CHashableKey TKey, typename TValue>
    using Collections::ConcurrentFlatMap;
        
    template <typename TValue>
    using Collections::ConcurrentFlatIdentifierMap;

    // double buffered
    
    template <CHashableKey TKey, typename TValue>
    using Collections::DoubleBufferedSnapshotMap;
        
    template <typename TValue>
    using Collections::DoubleBufferedSnapshotIdentifierMap;
    
    // data container
    
    template <typename TValue>
    using Collections::DataContainer;
    
    template <typename TValue>
    [[deprecated("WritableDataContainer is just an alias, use DataContainer instead")]]
    using Collections::WritableDataContainer;
    
    template <typename TValue>
    using Collections::ConstantDataContainer;
}

export namespace std {
    // not implemented
    // template<>
    // using std::hash<Stellar::Native::Core::Data::Registry::IdentifierRegistry>;
    
    // not implemented
    // template<>
    // using std::hash<Stellar::Native::Core::Data::Registry::LabelRegistry>;
}