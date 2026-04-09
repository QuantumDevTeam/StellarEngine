using System;
using Stellar.Kernel.Quantization;

namespace Stellar.Kernel.FileSystem
{
    /// <summary>
    /// Represents the address of a file within a specific domain.
    /// </summary>
    /// <remarks>
    /// <para>A location combines a <see cref="IDomain"/> and a relative <see cref="Path"/> string.
    /// It uniquely identifies a file in the quantum file system.</para>
    /// <para>Locations are meta‑quants and equatable. They can be created by combining a domain and a path,
    /// or by parsing a string representation (implementation‑specific).</para>
    /// </remarks>
    public interface ILocation
        : IMetaQuant, IEquatable<ILocation>
    {
        /// <summary>
        /// Gets the domain that contains this location.
        /// </summary>
        IDomain Domain { get; }

        /// <summary>
        /// Gets the relative path of the file inside the domain.
        /// </summary>
        string Path { get; }

        /// <summary>
        /// Returns the full location as a string (typically "domain://path" or similar).
        /// </summary>
        /// <returns>The string representation of the location.</returns>
        string ToString();
    }
}