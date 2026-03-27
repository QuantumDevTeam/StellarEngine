using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Stellar.ProjectAnalyzer;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class EntryPointAnalyzer : DiagnosticAnalyzer
{
    public const string DiagnosticId = "STELLAR001";

    private static readonly LocalizableString Title =
        new LocalizableResourceString(nameof(Resources.STELLAR001Title), Resources.ResourceManager,
            typeof(Resources));

    private static readonly LocalizableString MessageFormat =
        new LocalizableResourceString(nameof(Resources.STELLAR001MessageFormat), Resources.ResourceManager,
            typeof(Resources));

    private const string Category = "Stellar";

    private static readonly LocalizableString Description =
        new LocalizableResourceString(nameof(Resources.STELLAR001Description), Resources.ResourceManager,
            typeof(Resources));

    private static readonly DiagnosticDescriptor Rule = new(DiagnosticId, Title, MessageFormat, Category,
        DiagnosticSeverity.Warning, isEnabledByDefault: true, description: Description);

    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } =
        ImmutableArray.Create(Rule);

    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.EnableConcurrentExecution();
        context.RegisterCompilationAction(CompilationAction);
        context.RegisterSyntaxNodeAction(SyntaxNodeAction, SyntaxKind.ClassDeclaration);
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

    private bool HasAttribute(INamedTypeSymbol classSymbol, string attrName)
    {
        return classSymbol.GetAttributes()
            .Any(attr => attr.AttributeClass?.Name == attrName ||
                         attr.AttributeClass?.ToDisplayString() == attrName);
    }

    private bool InheritsFrom(INamedTypeSymbol classSymbol, string baseName)
    {
        var current = classSymbol.BaseType;
        while (current != null)
        {
            if (current.Name == baseName || current.ToDisplayString() == baseName)
                return true;
            current = current.BaseType;
        }

        return false;
    }

    private void SyntaxNodeAction(SyntaxNodeAnalysisContext context)
    {
        // 1. Get the class declaration syntax
        if (context.Node is not ClassDeclarationSyntax classDeclarationNode)
            return;

        // 2. Get the semantic model and the declared symbol for the class
        var semanticModel = context.SemanticModel;
        var classSymbol = semanticModel.GetDeclaredSymbol(classDeclarationNode) as INamedTypeSymbol;
        if (classSymbol == null)
            return;

        // 3. Perform the checks using the symbol
        bool hasAttr = HasAttribute(classSymbol, "StellarEntryPointAttribute");
        bool inherits = InheritsFrom(classSymbol, "StellarEntryPoint");

        // 4. Report diagnostic if the class does NOT have the attribute OR does NOT inherit correctly
        //    (Your original logic: if both true, do nothing; otherwise report)
        if (!hasAttr || !inherits)
        {
            // Report at the class identifier location for better visibility
            var location = classDeclarationNode.Identifier.GetLocation();
            context.ReportDiagnostic(Diagnostic.Create(Rule, location));
        }
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
                var symbol =
                    ModelExtensions.GetDeclaredSymbol(compilation.GetSemanticModel(tree), classDecl) as ITypeSymbol;
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