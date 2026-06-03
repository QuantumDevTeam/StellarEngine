// StellarEngine
// Copyright (c) 2026 QuantumDevTeam
// SPDX-License-Identifier: PolyForm-Noncommercial-1.0.0
module;

#include "pch.h"

#include "IdentifierRegistry.h"
#include "LabelRegistry.h"

export module Stellar.Native.Core.Data.Regostry;

export namespace Stellar::Native::Core::Data::Registry {
    // not implemented
    // using Registry::to_string;
    
    // IdentifierRegistry
    using Registry::IdentifierRegistry;
    
    // LabelRegistry
    using Registry::LabelRegistry;
}

export namespace std {
    // not implemented
    // template<>
    // using std::hash<Stellar::Native::Core::Data::Registry::IdentifierRegistry>;
    
    // not implemented
    // template<>
    // using std::hash<Stellar::Native::Core::Data::Registry::LabelRegistry>;
}