# StellarEngine — Versioning Policy

## 1. General Concept

StellarEngine uses **intentional and minimalistic versioning**.

A version is **the name of an engine state** that can be:

* referenced
* built
* demonstrated
* used as a stable point of comparison

Versions are **not equal to commits** and are **never created automatically**.

---

## 2. Where Versions Exist

A version exists **only as a Git tag**.

* Commits do not contain version numbers
* Branches do not contain version numbers
* Version = tag name

---

## 3. Current Project Stage (Pre-release)

Until the first stable release (`v1.0.0`), StellarEngine is in an active **development stage**.

The following scheme is used:

```
v0.X.Y
```

Where:

* `0` — the project does not provide a stable public API
* `X` — breaking changes (backward compatibility may break)
* `Y` — accumulated changes within the same development stage

> During the `0.x` period, any API changes are allowed without compatibility guarantees.

---

## 4. When a New Version Is Created

A new version is created when:

* a **logically complete set of changes** has accumulated
* the current state can be described in a single sentence
* the state is meaningful as a reference point

A version is **not created**:

* for every commit
* on a timer
* "just because it has been a while"

---

## 5. Pre-release Versions

For unstable, testing, or transitional states, pre-release modifiers are used:

```
v0.2.0-alpha
v0.2.0-beta
v0.2.0-rc.1
```

A pre-release version:

* is considered a distinct version
* is not equivalent to the final version without a suffix

---

## 6. Transition to v1.0.0

Version `v1.0.0` indicates that:

* a baseline public API has been formed
* core subsystems are stabilized
* the project is ready for external usage

After `v1.0.0`, the project switches to classic Semantic Versioning:

```
MAJOR.MINOR.PATCH
```

---

## 7. Backward Compatibility

Before `v1.0.0`:

* backward compatibility is **not guaranteed**

After `v1.0.0`:

* MAJOR — breaking changes
* MINOR — API extensions
* PATCH — bug fixes and internal improvements

---

Also see:
- [GitPolicy.md](GitPolicy.md)
- [CONTRIBUTING.md](CONTRIBUTING.md)