// StellarEngine
// Copyright (c) 2026 QuantumDevTeam
// SPDX-License-Identifier: PolyForm-Noncommercial-1.0.0
#ifndef PCH_H
#define PCH_H

#define WIN32_LEAN_AND_MEAN
#define NOMINMAX

#pragma warning(disable: 4068) // unknown pragma (for clang)
#pragma warning(disable: 4003) // unspecified attribute in macros

// most used lib's
#include <windows.h>
#include <combaseapi.h>
#include <cstdint>
#include <array>
#include <vector>
#include <string>
#include <string_view>
#include <format>
#include <compare>
#include <span>
#include <optional>
#include <mutex>

// lib specific lib's
#include <chrono>

// custom directives
#include "include/Stellar.Native.Core/Extensions.h"
#include "include/Stellar.Native.TimingSystem/Extensions.h"

#endif
