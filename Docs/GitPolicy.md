# StellarEngine — Git & Collaboration Policy

## 1. Purpose of This Document

This document defines **principles for working with Git**, not a rigid set of rules.

Its goals are to:

* keep the project history readable
* reduce chaos as the team grows
* ensure transparency of development decisions

---

## 2. Commits

### 2.1 What a Commit Is

A commit represents **one logically complete change**.

A good commit answers the question:

> *What changed and why?*

---

### 2.2 Commit Frequency

The following is allowed and encouraged:

* frequent commits
* small commits
* focused commits

Committing every 5–15 minutes is **normal practice**, as long as the change is logically complete.

---

### 2.3 Commit Message Format

A simple and explicit format is used:

```
[ModuleShortName] <verb> <what changed>
```

Examples:

```
[GW] add graphics pipeline abstraction
[CM] refactor core module initialization order
[RPM] fix memory leak in resource manager
```

Version numbers **must not** appear in commit messages.

---

## 3. Branches

The project allows the following branch types:

* `main` — stable integration branch
* `develop` — active development and integration
* `feature/*` — development of specific systems or technologies
* `experiment/*` — temporary or high-risk research work

Branches may be:

* deleted without preservation
* rewritten
* abandoned

This is expected and acceptable.

---

## 4. Merging

Before merging into `main`:

* the code must build
* changes must be logically complete
* commit history must remain readable

Versions (Git tags) are **created only on `main`**.

---

## 5. Git Tags

### 5.1 Version Tags

A version tag:

* follows a version format (e.g. `v0.x.y`)
* represents a product state
* is created intentionally

One commit corresponds to **one product version**.

---

### 5.2 Service / Milestone Tags

Service tags are used **sparingly** and indicate architectural or project-level events.

Examples:

* introduction of a new major subsystem
* architectural turning points
* start or completion of a large development phase

Service tags:

* are not versions
* do not replace commits
* are not used to classify modules

---

## 6. Team Scaling

As the team grows:

* commit rules remain unchanged
* versions are created centrally
* architectural decisions are documented, not inferred from commits

In case of conflict:

> **History readability takes priority over speed.**

---

## 7. Responsibility Principle

Each contributor:

* is responsible for the clarity of their commits
* is not required to perfectly predict the future
* is required not to create chaos in the present

---

Also see:
- [Versioning.md](Versioning.md)
- [CONTRIBUTING.md](CONTRIBUTING.md)