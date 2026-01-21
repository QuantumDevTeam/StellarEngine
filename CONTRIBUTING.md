# Contributing to StellarEngine

Thank you for your interest in contributing to **StellarEngine**.

StellarEngine is a long-term engine project developed with a strong focus on architectural research, modularity, and sustainability. Contributions are welcome, but contributors are expected to understand and respect the core design principles described below.

---

## Core Philosophy

StellarEngine is built around a **hypermodular, bottom-up architecture**.

Modules are treated as first-class system components and are **not bound to a single fixed layer** such as runtime, editor, or tooling. A module may:

* Be reused across multiple contexts
* Exist simultaneously in runtime and editor environments
* Be embedded into standalone tools (installers, pipelines, debuggers)
* Appear unused, experimental, or partially integrated

This behavior is **intentional** and represents a core architectural principle of the engine.

Contributors should not assume traditional engine boundaries. Attempts to "simplify" the system by enforcing rigid layer separation without discussion are discouraged.

---

## Project Status

StellarEngine is currently in active development.

* APIs are unstable
* Internal architecture may change
* Breaking changes are expected

By contributing, you acknowledge that the project is evolving and that some work may need to be revised or refactored in the future.

---

## Contribution Scope

Contributions may include, but are not limited to:

* Core engine modules
* Experimental subsystems
* Tooling and infrastructure
* Documentation
* Prototypes and research implementations

Not all contributions are expected to be production-ready. Research-oriented or exploratory work is acceptable when clearly marked and documented.

---

## Architectural Changes

Significant architectural changes must be discussed before implementation.

If your contribution:

* Introduces new core concepts
* Alters module interaction patterns
* Redefines subsystem responsibilities

please open an issue or discussion describing the motivation, design, and trade-offs.

---

## Coding Guidelines

* Follow existing code style and conventions
* Prefer clarity over premature optimization
* Keep modules loosely coupled
* Avoid unnecessary dependencies between modules

Hypermodularity relies on explicit boundaries and well-defined interfaces.

---

## Commit and Workflow Rules

* Keep commits focused and atomic
* Do not include version numbers in commit messages
* Use descriptive commit messages (e.g. `[CSN] Add node graph evaluator module`)
* Versioning is handled exclusively via Git tags

Refer to [GitPolicy.md](GitPolicy.md) and [Versioning.md](Versioning.md) for detailed workflow and versioning rules.

---

## Licensing and Contributions

By contributing to StellarEngine, you agree that your contributions may be used as as part of the StellarEngine project developed by QuantumDev team under its current and future licensing models.

At the current stage, the project is distributed under the MIT License. Licensing terms may evolve as the project approaches production readiness.

---

## Final Notes

StellarEngine values thoughtful engineering, architectural discussion, and long-term thinking.

If you are unsure about a contribution, open a discussion first. Collaboration and communication are preferred over silent assumptions.

### Welcome aboard.

---

Also see [README.md](README.md)
