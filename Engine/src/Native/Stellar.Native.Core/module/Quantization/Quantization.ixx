// StellarEngine
// Copyright (c) 2026 QuantumDevTeam
// SPDX-License-Identifier: PolyForm-Noncommercial-1.0.0
module;

#include "pch.h"

#include "IMetaQuant.h"
#include "MetaQuant.h"

export module Stellar.Native.Core.Quantization;

export namespace Stellar::Native::Core::Quantization {
    using Quantization::to_string;
    
    // IMetaQuant
    using Quantization::IMetaQuant;
    
    // MetaQuant
    using Quantization::MetaQuant;
}

export namespace std {
    template<>
    using std::hash<Stellar::Native::Core::Quantization::IMetaQuant>;
    
    template<>
    using std::hash<Stellar::Native::Core::Quantization::MetaQuant>;
}