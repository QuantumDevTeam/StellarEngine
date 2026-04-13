// ReSharper disable ConvertToExtensionBlock

using System.Linq;
using Microsoft.CodeAnalysis;

namespace Stellar.ProjectAnalyzer.Internal;

internal static class RoslynExtensions
{
    /// <summary>
    /// Returns <c>true</c> when <paramref name="type"/> directly or indirectly
    /// inherits from <paramref name="baseType"/>.
    /// </summary>
    public static bool InheritsFrom(this INamedTypeSymbol type, INamedTypeSymbol baseType)
    {
        var current = type.BaseType;
        while (current is not null)
        {
            if (SymbolEqualityComparer.Default.Equals(current, baseType))
                return true;
            current = current.BaseType;
        }

        return false;
    }

    /// <summary>
    /// Returns <c>true</c> when <paramref name="type"/> is decorated with
    /// <paramref name="attributeType"/> (direct attributes only, no inheritance).
    /// </summary>
    public static bool HasAttribute(this INamedTypeSymbol type, INamedTypeSymbol attributeType) =>
        type.GetAttributes()
            .Any(a => SymbolEqualityComparer.Default.Equals(a.AttributeClass, attributeType));
}