using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Stellar.ProjectAnalyzer;

public class EntryPointAnalyzer : DiagnosticAnalyzer
{
    private static readonly DiagnosticDescriptor Rule = new(
        "STELLAR001",
        "Entry point not found",
        "No class with [StellarEntryPoint] attribute inheriting StellarEntryPoint found",
        "Stellar",
        DiagnosticSeverity.Error,
        true);

    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);

    public override void Initialize(AnalysisContext context)
    {
        context.EnableConcurrentExecution();
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.RegisterCompilationAction(CompilationAction);
    }

    private bool HasAttribute(ITypeSymbol symbol, INamedTypeSymbol attr) =>
        symbol.GetAttributes().Any(a =>
            a.AttributeClass != null && a.AttributeClass.Equals(attr, SymbolEqualityComparer.Default));

    private bool InheritsFrom(ITypeSymbol symbol, string baseName)
    {
        var baseType = symbol.BaseType;
        while (baseType != null)
        {
            if (baseType.Name == baseName || baseType.ToDisplayString() == baseName)
                return true;
            baseType = baseType.BaseType;
        }

        return false;
    }

    private void CompilationAction(CompilationAnalysisContext context)
    {
        var compilation = context.Compilation;
        var entryPointAttr = compilation.GetTypeByMetadataName("Stellar.Kernel.EntryPoint.StellarEntryPointAttribute");
        if (entryPointAttr == null) return;

        bool found = false;
        foreach (var tree in compilation.SyntaxTrees)
        {
            var root = tree.GetRoot();
            var classes = root.DescendantNodes().OfType<ClassDeclarationSyntax>();
            foreach (var classDecl in classes)
            {
                var symbol = compilation.GetSemanticModel(tree).GetDeclaredSymbol(classDecl) as ITypeSymbol;
                if (symbol != null
                    && HasAttribute(symbol, entryPointAttr)
                    && InheritsFrom(symbol, "StellarEntryPoint"))
                {
                    found = true;
                    break;
                }
            }
        }

        if (!found)
            context.ReportDiagnostic(Diagnostic.Create(Rule, Location.None));
    }
}