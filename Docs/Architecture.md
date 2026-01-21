# StellarEngine Architecture

> This document describes the architectural principles of StellarEngine.
> It is intended to explain *how the system is structured* and *why it is structured this way*,
> without going into low-level implementation details.

---

## 1. What StellarEngine Is

StellarEngine is a **platform**, not a monolithic game engine.

It is designed as a system composed of a minimal contractual kernel layer, a set of reusable modules, and multiple
execution contexts. The
same modules may be reused across different products and tools without duplication or special-casing.

---

## 2. Hypermodular Design

StellarEngine follows a **hypermodular** architectural model.

In this model, modules are not bound to a single execution layer.

A module does not belong to:

* runtime
* editor
* tools

A module belongs to:

* the system

Execution context defines **how** a module is used, not **where** it is implemented.

This allows the same module to be embedded into a game runtime, editor tooling, installers, or headless utilities.

---

## 3. Bottom-Up Architecture

StellarEngine is designed bottom-up.

Instead of enforcing rigid top-down layers, the architecture evolves from practical usage patterns. There is no
privileged layer within the system.

The editor, runtime, tools, and installer are all clients of the same underlying platform.

---

## 4. Kernel and Runtime

The **Kernel** is the smallest and most stable *contractual* part of the system.

The Kernel is **not** an engine module, not an engine core, and does not contain the main business logic.

Kernel responsibilities are limited to:

- defining core interfaces and contracts
- lifecycle and execution contracts
- cross-module agreements
- integration boundaries between SDK, runtime, editor, and tools
- minimal glue logic required for system composition

The Kernel defines *what exists* in the system, not *how it behaves*.

The Kernel is shared across multiple execution contexts and must remain thin, stable, and implementation-agnostic.

Kernel assemblies **must not** depend on engine modules, native implementations, or execution contexts.

The **runtime** is a composition of modules built on top of Kernel contracts and concrete implementations provided by
the Core and other modules.

Graphics, physics, scripting, networking, and other systems exist as replaceable modules rather than intrinsic engine
layers.

---

## 5. Modules

A module is a reusable system component with an explicit contract.

Modules:

* avoid implicit global state
* define clear boundaries
* may be dynamically loaded

Some modules may appear experimental, partially integrated, or unused. This is intentional and reflects the
research-driven nature of the platform.

---

## 6. Execution Contexts

An execution context defines how the system is composed and executed.

Examples of execution contexts include:

* Installer
* SDK build tools
* Editor
* Game runtime
* Asset pipeline
* Headless utilities

Contexts determine:

* which modules are loaded
* how modules are wired together
* how lifecycle is managed

Modules themselves remain context-agnostic.

---

## 7. Installer and Bootstrap

The installer is a **bootstrap system**, not the engine itself.

Its responsibilities include:

* verifying system prerequisites
* unpacking sources or binaries
* invoking build tools
* installing runtime components

The installer may use engine modules, but it must not depend on an already installed engine runtime.

---

## 8. SDK

The SDK is a developer-facing layer.

Its responsibilities include:

* project templates
* build targets
* integration with .NET tooling

The SDK does not define engine behavior. It defines how developers interact with the engine platform.

---

## 9. Editor

The editor is a client of the engine platform.

It uses engine modules, embeds runtime components where appropriate, and does not own engine logic. The editor is not
required for runtime operation.

---

## 10. Runtime Distribution Model

StellarEngine supports two primary distribution modes.

### Development Mode

* engine source code is available
* runtime can be rebuilt
* local binaries may override packaged ones

### Runtime Mode

* engine source code is not present
* only prebuilt binaries are used
* multiple products may share a single runtime installation

---

## 11. Product Philosophy

StellarEngine is developed as a product.

Research and experimentation are part of the development process, but they do not replace intentional design or
documentation.

Architectural decisions are documented explicitly rather than inferred from implementation details.

---

## 12. Non-Goals

StellarEngine does not aim to:

* enforce a single workflow
* hide complexity at all costs
* lock users into a specific product ecosystem

---

This document intentionally avoids low-level technical details. Those are expected to evolve alongside the
implementation.
