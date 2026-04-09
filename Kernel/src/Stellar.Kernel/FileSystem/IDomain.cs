using System;
using Stellar.Kernel.Label;
using Stellar.Kernel.Quantization;

namespace Stellar.Kernel.FileSystem
{
    /// <summary>
    /// Represents an abstract location scope (domain) that groups files.
    /// </summary>
    /// <remarks>
    /// <para>A domain defines a root or a container for files. Examples: a physical folder, an assembly,
    /// a ZIP archive, or a network share. Each domain has a type (<see cref="DomainType"/>) and a string value
    /// (e.g., absolute path or assembly name).</para>
    /// <para>Domains are meta‑quants (<see cref="IMetaQuant"/>), labeled (<see cref="ILabeled"/>), and equatable.</para>
    /// </remarks>
    public interface IDomain
        : IMetaQuant, ILabeled, IEquatable<IDomain>
    {
        /// <summary>
        /// Gets the type of this domain (e.g., directory, assembly).
        /// </summary>
        DomainType Type { get; }

        /// <summary>
        /// Gets the domain's native value (e.g., full directory path, assembly name).
        /// </summary>
        string Value { get; }

        /// <summary>
        /// Returns the domain as a string (typically the <see cref="Value"/>).
        /// </summary>
        /// <returns>The domain string representation.</returns>
        string ToString();
    }
}