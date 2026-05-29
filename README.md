# StellarEngine

Licensed under PolyForm Noncommercial 1.0.0\
StellarEngine may include contributions from external community
contributors under the terms described in CLA.md.

![Logo](Data/Logo.ico)

StellarEngine is a hypermodular game engine designed with a bottom-up 
architectural philosophy.

The engine treats modules as first-class system components rather than 
rigidly separated layers (runtime, editor,
tools). This allows engine modules to be embedded across different 
contexts: inside games, editors, tooling pipelines,
installers, and custom workflows.

StellarEngine is developed as a long-term product, while openly 
embracing research-and-development driven evolution.

---

## Core Concept: Hypermodular Architecture

StellarEngine is built around a **hypermodular**, **bottom-up** 
architecture.

Instead of enforcing strict boundaries between runtime, editor, and 
tooling layers, StellarEngine allows modules to be
reused and embedded freely across system contexts.

This design enables:

* Using runtime engine modules directly inside editor tooling
* Embedding editor UI widgets directly into games
* Building standalone tools (installers, debuggers, pipelines) using 
engine modules
* Composing products from the same technological foundation

Modules may appear reusable in unexpected contexts. This is 
intentional.

---

## Project Vision

StellarEngine is developed and maintained by QuantumDevTeam as a 
long-term technology platform.

StellarEngine aims to become a flexible technological foundation for 
building games, tools, and interactive systems.

The engine is not designed as a fixed framework, but as a composable 
system where architecture grows from practical
usage rather than predefined top-down constraints.

---

## Current Status

StellarEngine is currently in an early development stage.

* APIs are unstable
* Architecture is actively evolving
* Breaking changes are expected

The project is not yet production-ready, but is actively shaped 
toward a minimal viable engine foundation.

---

## Repository Structure

This repository contains the core engine sources, experimental 
modules, documentation, and development policies.

Detailed architectural structure will evolve as the engine matures.

---

## Documentation

License and Contributing. See these documents before submitting contributions.

- [License](LICENSE)
- [Contributing Guide](CONTRIBUTING.md)
- [Contributor License Agreement (CLA)](CLA.md)

Architecture:

- [Architecture Overview](Docs/Architecture/Overview.md)
- [Architecture Core](Docs/Architecture/Core.md)
- [Pipeline Overview](Docs/Architecture/Pipeline.md)

Philosophy

- [Architecture Philosophy](Docs/Architecture.md)
- [Design Philosophy](Docs/DesignPhilosophy.md)

Workflow Policy:

- [Git & Workflow Policy](Docs/GitPolicy.md)
- [Versioning Strategy](Docs/Versioning.md)

---

## License

StellarEngine is licensed under the 
PolyForm Noncommercial License 1.0.0.

You may use, study, modify, and distribute the engine for 
noncommercial purposes under the terms of the license.

Commercial use of StellarEngine or derivative works is prohibited 
without explicit written permission from the project
owner.

Official license text:
https://polyformproject.org/licenses/noncommercial/1.0.0/

See [LICENSE](LICENSE) for full terms.

---

### Commercial Licensing

Future commercial licensing options may become available as 
StellarEngine approaches production readiness.

If you are interested in commercial usage, integration, or 
partnership opportunities, please contact QuantumDevTeam.

---

## Disclaimer

StellarEngine is an evolving system. Some components may be 
experimental, incomplete, or subject to redesign.

This is a deliberate part of the engine's development philosophy.
