#pragma warning disable RS1037 // Add "CompilationEnd" custom tag to the diagnostic descriptor used to initialize field 'EntryPointMissingAttribute' as it is used to report a compilation end diagnostic
#pragma warning disable RS1032 // The diagnostic message should not contain any line return character nor any leading or trailing whitespaces and should either be a single sentence without a trailing period or a multi-sentences with a trailing period

using Microsoft.CodeAnalysis;

namespace Stellar.ProjectAnalyzer.Diagnostics;

internal static class StellarDiagnosticDescriptors
{
    private const string EntryPointCategory = "Stellar.EntryPoint";

    // ── STLR0101 ─────────────────────────────────────────────────────────

    internal static readonly DiagnosticDescriptor EntryPointTypeNotFound = new(
        id: StellarDiagnosticIds.EntryPointTypeNotFound,
        title: "Entry-point type not found",
        messageFormat: "Type '{0}' specified as StellarEntryPoint in project config was not found",
        category: EntryPointCategory,
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description:
        "The fully-qualified type name written in .stellar.project[Project][StellarEntryPoint] " +
        "must exist in the current module. " +
        "If you want the default convention ({AssemblyName}.EntryPoint), use the value \"default\"."
    );

    // ── STLR0102 ─────────────────────────────────────────────────────────

    internal static readonly DiagnosticDescriptor EntryPointMissingInheritance = new(
        id: StellarDiagnosticIds.EntryPointMissingInheritance,
        title: "Entry-point must inherit StellarEntryPoint",
        messageFormat: "Type '{0}' is configured as the module entry point but does not inherit " +
                       "'Stellar.Kernel.EntryPoint.StellarEntryPoint'",
        category: EntryPointCategory,
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description:
        "An entry-point class must extend StellarEntryPoint so the engine can manage its lifecycle."
    );

    // ── STLR0103 ─────────────────────────────────────────────────────────

    internal static readonly DiagnosticDescriptor EntryPointMissingAttribute = new(
        id: StellarDiagnosticIds.EntryPointMissingAttribute,
        title: "Entry-point missing [StellarEntryPoint] attribute",
        messageFormat: "Type '{0}' is configured as the module entry point but is not decorated " +
                       "with [StellarEntryPoint]",
        category: EntryPointCategory,
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description:
        "Apply [StellarEntryPoint] to let the engine discover this class during assembly scanning."
    );

    // ── STLR0104 ─────────────────────────────────────────────────────────

    internal static readonly DiagnosticDescriptor NoEntryPointInModule = new(
        id: StellarDiagnosticIds.NoEntryPointInModule,
        title: "No entry point defined in module",
        messageFormat: "Module '{0}' has no type decorated with [StellarEntryPoint]. " +
                       "Define an entry-point class or check .stellar.project",
        category: EntryPointCategory,
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true,
        description:
        "Every Stellar module must declare exactly one entry-point — a class that inherits " +
        "StellarEntryPoint and is marked with [StellarEntryPoint]."
    );
}