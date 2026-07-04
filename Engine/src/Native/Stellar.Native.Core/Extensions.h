// StellarEngine
// Copyright (c) 2026 QuantumDevTeam
// SPDX-License-Identifier: PolyForm-Noncommercial-1.0.0
#pragma once

// pragma

#define STELLAR_PRAGMA(x) _Pragma(#x)

#define STELLAR_CLANG_IGNORE_START()\
STELLAR_PRAGMA(clang diagnostic push)

#define STELLAR_CLANG_IGNORE_ADD(which)\
STELLAR_PRAGMA(clang diagnostic ignored which)

#define STELLAR_CLANG_IGNORE_END()\
STELLAR_PRAGMA(clang diagnostic pop)

#define STELLAR_CLANG_IGNORE(which)\
STELLAR_CLANG_IGNORE_START() \
STELLAR_CLANG_IGNORE_ADD(which)
