using System;
using Stellar.Kernel.Quantization;

namespace Stellar.Kernel.FileSystem
{
    /// <summary>
    /// Represents an abstract quantum file stored at a quantum location.
    /// </summary>
    /// <remarks>
    /// <para>A file is identified by its <see cref="Location"/> and has a <see cref="Type"/>
    /// (e.g., text, binary, image). Files are meta‑quants and can be compared for equality.</para>
    /// <para>This interface does not provide direct I/O; use <see cref="IFileStream"/> or file provider methods.</para>
    /// </remarks>
    public interface IFile
        : IMetaQuant, IEquatable<IFile>
    {
        /// <summary>
        /// Gets the location of the file (domain + path).
        /// </summary>
        ILocation Location { get; }

        /// <summary>
        /// Gets the file type descriptor.
        /// </summary>
        IFileType Type { get; }
    }
}