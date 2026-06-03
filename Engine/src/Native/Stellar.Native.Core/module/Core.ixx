// StellarEngine
// Copyright (c) 2026 QuantumDevTeam
// SPDX-License-Identifier: PolyForm-Noncommercial-1.0.0
module;

#include "pch.h"

#include "Identifier.h"
#include "Label.h"

export module Stellar.Native.Core;

export namespace Stellar::Native::Core {
    using Core::to_string;
    
    // identifier
    using Core::Identifier;
    using Core::NullIdentifier;
    
    // Label
    using Core::Label;
    using Core::NullLabel;
    using Core::UnnamedUnboundLabel;
}

export namespace std {
    template<>
    using std::hash<Stellar::Native::Core::Identifier>;
    
    template<>
    using std::hash<Stellar::Native::Core::Label>;
}