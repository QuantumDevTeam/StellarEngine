using System;
using Stellar.Kernel.Label;
using Stellar.Kernel.Quantization;

namespace Stellar.Kernel.FileSystem
{
    /// <summary>
    /// Describes the format or kind of quantum file.
    /// </summary>
    /// <remarks>
    /// <para>File types are registrable meta‑quants (<see cref="IRegistrableMetaQuant"/>), labeled,
    /// and equatable. They can be used to associate MIME types, extensions, or custom handlers.</para>
    /// <para>Examples: "text/plain", "image/png", "application/json".</para>
    /// </remarks>
    public interface IFileType
        : IRegistrableMetaQuant, ILabeled, IEquatable<IFileType>
    {
        /// <summary>
        /// Returns the file type as a string (e.g., its name or MIME type).
        /// </summary>
        /// <returns>The string representation of the file type.</returns>
        string ToString();
    }
}