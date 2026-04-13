namespace Stellar.ProjectAnalyzer.Configuration;

// ─────────────────────────────────────────────────────────────────────────
// Intentionally NOT referencing Stellar.Tools.Configuration here.
// Roslyn analyzers run inside the IDE / compiler host process. Loading
// arbitrary assemblies in that context causes version conflicts.
// These are thin, analyzer-private mirrors of the public DTOs.
// ─────────────────────────────────────────────────────────────────────────

internal sealed class StellarAnalyzerConfigFile
{
    public StellarAnalyzerProjectConfig? Project { get; set; }

    // Runtime section intentionally omitted — analyzers don't need it.
}

internal sealed class StellarAnalyzerProjectConfig
{
    public string? StellarEntryPoint { get; set; }

    // Other fields (Assets, Localizations …) will be added here
    // as future analyzers need them — zero cost when unused.
}