# Core Architecture

- [README.md](./../../README.md)

### Level 0 - Kernel
The core of the contracts between Design-time, Build-time and Run-Time
- Contracts
- Decoupling dependencies
- The basis of hyper-quants

---

### Level 1 - Stellar.Core Module
The core of the engine, the implementation of the basic concepts
- Registries
- Basic classes
- ID system

---

### Level 2 — Core Modules
They communicate only through the Kernel and Stellar.Core, completely independent of each other in Build-time,
but rely on collaboration in Run-time
- Logging
- EventSystem
- TaskSystem
- FileSystem
- TimeSystem
- AssetPipeline

---

### Level 3 — Stellar.RuntimeSystem Module
Creating the foundation for the engine core to work
- Loading modules
- Initialization and de-initialization
- Start and stop control

---

### Level 4 — High-Level Modules
High-level modules that provide ready-made tools for working with the engine- Network
- Graphic
- Graphic.UI
- VR
- Physics
- Stage

### Level 4.1 - Stellar.Engine project
The main module is included in the end modules. This project is the "engine"
- Aggregation of modules
- Optimization of the finished assembly files
- Configo-dependent subsystems

### Level 5 - EndPoint
- The end user of the engine
- custom modules using the Stellar.SDK
- Game / program code
