namespace Stellar.ProjectAnalyzer.Diagnostics;

/// <summary>
/// Central registry of all Stellar diagnostic IDs.
/// Convention: STLR + 4-digit category + sequential number.
/// 01xx — EntryPoint, 02xx — (future) Assets, 03xx — (future) Localizations …
/// </summary>
public static class StellarDiagnosticIds
{
    // ── EntryPoint ────────────────────────────────────────────────────────

    /// <summary>Type specified in config[Project][StellarEntryPoint] not found in compilation.</summary>
    public const string EntryPointTypeNotFound = "STLR0101";

    /// <summary>Configured entry-point type does not inherit StellarEntryPoint base class.</summary>
    public const string EntryPointMissingInheritance = "STLR0102";

    /// <summary>Configured entry-point type is not decorated with [StellarEntryPoint] attribute.</summary>
    public const string EntryPointMissingAttribute = "STLR0103";

    /// <summary>No entry-point types (with [StellarEntryPoint]) found anywhere in the module.</summary>
    public const string NoEntryPointInModule = "STLR0104";
}